using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StortyBack.Data
{
    public class StoryData : MainData
    {
        public int? id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StoryContent { get; set; }
        public DateTime PostedOn { get; set; }
        public int UserID { get; set; }

        public List<GroupData> Groups { get; set; }


        public bool SaveStory(StoryData sm)
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

        public List<StoryData> GetStories(int userID)
        {
            var stories = db.Stories.AsNoTracking().Where(x => x.UsersID == userID).ToList();
            List<StoryData> sm = new List<StoryData>();

            foreach (var item in stories)
            {
                sm.Add(new StoryData()
                {
                    id = item.id,
                    Description = item.Description,
                    PostedOn = item.PostedOn,
                    StoryContent = item.StoryContent,
                    Title = item.Title
                });
            }
            sm.Reverse();
            return sm;
        }

        public StoryData GetStory(int storyID)
        {
            var story = db.Stories.AsNoTracking().Where(x => x.id == storyID).FirstOrDefault();
            StoryData sm = new StoryData()
            {
                id = story.id,
                Description = story.Description,
                PostedOn = story.PostedOn,
                StoryContent = story.StoryContent,
                Title = story.Title,
            };
            return sm;
        }
    }
}
