using StortyBack.Data;
using StorySocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorySocialNetwork.StorySNData
{
    public class StoryDataConvert
    {

        //public int CheckLogin(LoginModel lm)
        //{
        //    var userid = db.Users.Where(x => x.name == lm.Login && x.password == lm.Password).Select(s=>s.id).FirstOrDefault();
        //    return userid;
        //}

        //public bool SaveStory(StoryModel sm)
        //{
        //    try
        //    {
        //        Story story;
        //        if (sm.id == null)
        //        {
        //            story = new Story
        //            {
        //                UsersID = sm.UserID,
        //                Description = sm.Description,
        //                StoryContent = sm.StoryContent,
        //                Title = sm.Title,
        //                PostedOn = DateTime.Now
        //            };
        //            db.Stories.Add(story);
        //        }
        //        else
        //        {
        //            story = db.Stories.Where(x => x.id == sm.id).FirstOrDefault();
        //            story.UsersID = sm.UserID;
        //            story.Description = sm.Description;
        //            story.StoryContent = sm.StoryContent;
        //            story.Title = sm.Title;
        //        }

        //        var SelectedGroupIDs = sm.Groups.Where(x => x.isSelected == true).Select(s => s.id).ToArray();
        //        List<Group> gr = db.Groups.Where(x => SelectedGroupIDs.Contains(x.id)).ToList();
        //        story.Groups.Clear();
        //        foreach (var item in gr)
        //        {
        //            story.Groups.Add(item);
        //        }

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }

        //}

        //public List<StoryModel> GetStories(int userID)
        //{
        //    var stories = db.Stories.Where(x => x.UsersID == userID).ToList();
        //    List<StoryModel> sm = new List<StoryModel>();

        //    foreach (var item in stories)
        //    {
        //        sm.Add(new StoryModel()
        //        {
        //            id = item.id,
        //            Description = item.Description,
        //            PostedOn = item.PostedOn,
        //            StoryContent = item.StoryContent,
        //            Title = item.Title
        //        });
        //    }

        //    return sm;
        //}

        //public StoryModel GetStory(int storyID)
        //{

        //    var story = db.Stories.Where(x => x.id == storyID).FirstOrDefault();
        //    StoryModel sm = new StoryModel()
        //    {
        //        id = story.id,
        //        Description = story.Description,
        //        PostedOn = story.PostedOn,
        //        StoryContent = story.StoryContent,
        //        Title = story.Title,
        //    };
        //    return sm;
        //}

        //public List<GroupModel> GetGroups(int? id)
        //{

        //    var groups = db.Groups.ToList();
        //    List<Group> selected = new List<Group>();
        //    if (id != null)
        //    {
        //        selected = db.Groups.Where(u => u.Stories.Any(e => e.id == id.Value)).ToList() ;
        //    }
        //    List<GroupModel> gms = new List<GroupModel>();
        //    foreach (var item in groups)
        //    {
        //        bool isselected = selected.Where(s => s.id == item.id).ToList().Count > 0;
        //        GroupModel gm = new GroupModel()
        //        {
        //            id = item.id,
        //            Description = item.Description,
        //            Name = item.Name,
        //            isSelected = isselected
        //        };
        //        gms.Add(gm);
        //    }

        //    return gms;
        //}

        //public List<GroupModel> GetGroupsInfo()
        //{

        //    var groups = db.Groups.ToList();
        //    List<GroupModel> gms = new List<GroupModel>();

        //    foreach (var item in groups)
        //    {
        //        var usersCount = item.Stories.GroupBy(s => s.UsersID).Select(g=>g.Count()).ToList().Count();
        //        GroupModel gm = new GroupModel()
        //        {
        //            id = item.id,
        //            Description = item.Description,
        //            Name = item.Name,
        //            StoriesCount = item.Stories.Count,
        //            UsersCount = usersCount
        //        };
        //        gms.Add(gm);
        //    }

        //    return gms;
        //}
        /// <summary>
        /// StoryModel  convert to StoryData 
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public StortyBack.Data.StoryData StoryModelToData(StoryModel sm)
        {

            List<GroupData> gd = new List<GroupData>();
            foreach (var group in sm.Groups)
            {
                gd.Add(GroupModelToData(group));
            }

            StortyBack.Data.StoryData sd = new StortyBack.Data.StoryData()
            {
                id = sm.id,
                PostedOn = sm.PostedOn,
                StoryContent = sm.StoryContent,
                Title = sm.Title,
                Description = sm.Description,
                UserID = sm.UserID,
                Groups = gd
            };
            return sd;

        }
        /// <summary>
        /// StoryModel  convert to StoryData 
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public StoryModel StoryDataToModel(StortyBack.Data.StoryData sd)
        {

            List<GroupModel> gm = new List<GroupModel>();
            if (sd.Groups != null)
            {
                foreach (var group in sd.Groups)
                {
                    gm.Add(GroupDataToModel(group));
                }
            }

            StoryModel sm = new StoryModel()
            {
                id = sd.id,
                PostedOn = sd.PostedOn,
                StoryContent = sd.StoryContent,
                Title = sd.Title,
                Description = sd.Description,
                UserID = sd.UserID,
                Groups = gm
            };
            return sm;

        }
        /// <summary>
        /// GroupModel  convert to GroupData 
        /// </summary>
        /// <param name="gm"></param>
        /// <returns></returns>
        public GroupData GroupModelToData(GroupModel gm)
        {
            GroupData gd = new GroupData()
            {
                Description = gm.Description,
                id = gm.id,
                isSelected = gm.isSelected,
                Name = gm.Name,
                StoriesCount = gm.StoriesCount,
                UsersCount= gm.UsersCount                
            };
            return gd;
        }
        /// <summary>
        ///   GroupData convert to   GroupModel
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        public GroupModel GroupDataToModel(GroupData gd)
        {
            GroupModel gm = new GroupModel()
            {
                Description = gd.Description,
                id = gd.id,
                isSelected = gd.isSelected,
                Name = gd.Name,
                StoriesCount = gd.StoriesCount,
                UsersCount = gd.UsersCount
            };
            return gm;
        }
        /// <summary>
        ///  UserData convert to   LoginModel
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public UserData UserModelToData(LoginModel um)
        {
            UserData ud = new UserData()
            {
                Password = um.Password,
                UserName = um.Login
            };
            return ud;
        }
        /// <summary>
        ///  UserData convert to   LoginModel
        /// </summary>
        /// <param name="ud"></param>
        /// <returns></returns>
        public LoginModel UserDataToModel(UserData ud)
        {
            LoginModel um = new LoginModel()
            {
                Password = ud.Password,
                Login = ud.UserName
            };
            return um;
        }

    }
}