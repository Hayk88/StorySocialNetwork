using StorySocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorySocialNetwork.StorySNData
{
    public class StoryData
    {
        StorySNDataEFMContainer db = new StorySNDataEFMContainer();

        public int CheckLogin(LoginModel lm)
        {
            var userid = db.Users.Where(x => x.name == lm.Login && x.password == lm.Password).Select(s=>s.id).FirstOrDefault();
            return userid;
        }

        public bool SaveStory(StoryModel sm)
        {
            try
            {
                Story story;
                if (sm.id == null)
                {
                    story = new Story
                    {
                        UsersID = sm.UserID,
                        Description = sm.Description,
                        StoryContent = sm.StoryContent,
                        Title = sm.Title,
                        PostedOn = DateTime.Now
                    };
                    db.Stories.Add(story);
                }
                else
                {
                    story = db.Stories.Where(x => x.id == sm.id).FirstOrDefault();
                    story.UsersID = sm.UserID;
                    story.Description = sm.Description;
                    story.StoryContent = sm.StoryContent;
                    story.Title = sm.Title;
                }

                var SelectedGroupIDs = sm.Groups.Where(x => x.isSelected == true).Select(s => s.id).ToArray();
                List<Group> gr = db.Groups.Where(x => SelectedGroupIDs.Contains(x.id)).ToList();
                story.Groups.Clear();
                foreach (var item in gr)
                {
                    story.Groups.Add(item);
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public List<StoryModel> GetStories(int userID)
        {
            var stories = db.Stories.Where(x => x.UsersID == userID).ToList();
            List<StoryModel> sm = new List<StoryModel>();

            foreach (var item in stories)
            {
                sm.Add(new StoryModel()
                {
                    id = item.id,
                    Description = item.Description,
                    PostedOn = item.PostedOn,
                    StoryContent = item.StoryContent,
                    Title = item.Title
                });
            }

            return sm;
        }

        public StoryModel GetStory(int storyID)
        {

            var story = db.Stories.Where(x => x.id == storyID).FirstOrDefault();
            StoryModel sm = new StoryModel()
            {
                id = story.id,
                Description = story.Description,
                PostedOn = story.PostedOn,
                StoryContent = story.StoryContent,
                Title = story.Title,
            };
            return sm;
        }

        public List<GroupModel> GetGroups(int? id)
        {

            var groups = db.Groups.ToList();
            List<Group> selected = new List<Group>();
            if (id != null)
            {
                selected = db.Groups.Where(u => u.Stories.Any(e => e.id == id.Value)).ToList() ;
            }
            List<GroupModel> gms = new List<GroupModel>();
            foreach (var item in groups)
            {
                bool isselected = selected.Where(s => s.id == item.id).ToList().Count > 0;
                GroupModel gm = new GroupModel()
                {
                    id = item.id,
                    Description = item.Description,
                    Name = item.Name,
                    isSelected = isselected
                };
                gms.Add(gm);
            }

            return gms;
        }

        public List<GroupModel> GetGroupsInfo()
        {

            var groups = db.Groups.ToList();
            List<GroupModel> gms = new List<GroupModel>();

            foreach (var item in groups)
            {
                var usersCount = item.Stories.GroupBy(s => s.UsersID).Select(g=>g.Count()).ToList().Count();
                GroupModel gm = new GroupModel()
                {
                    id = item.id,
                    Description = item.Description,
                    Name = item.Name,
                    StoriesCount = item.Stories.Count,
                    UsersCount = usersCount
                };
                gms.Add(gm);
            }

            return gms;
        }
    }
}