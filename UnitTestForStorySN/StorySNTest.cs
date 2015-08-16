using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorySocialNetwork;
using StorySocialNetwork.StorySNData;
using StorySocialNetwork.Models;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestForStorySN
{
    [TestClass]
    public class StorySNTest
    {
        StoryData sd = new StoryData();
        [TestMethod]
        public void GetStoryTest()
        {
            int storyID = 1;
            var story = sd.GetStory(storyID);
            Assert.AreEqual((double)storyID, (double)story.id, 0, "id equel ");
        }

        [TestMethod]
        public void CheckLoginwrTest()
        {
            var userid = 1;
            LoginModel lm = new LoginModel(){
                Login = "hayk",
                Password= "hayk"
            };
            var userID = sd.CheckLogin(lm);
            Assert.AreEqual(userid, userID);
        }

        [TestMethod]
        public void LoadSaveStory()
        {
            GroupModel groupe = new GroupModel
            {
                id = 1,
                isSelected = true
            };
            List<GroupModel> lgm = new List<GroupModel>();
            lgm.Add(groupe);
            StoryModel sm = new StoryModel() { 
                Title =  "usnit Test Title",
                Description = "usnit Test Description",
                StoryContent = "usnit Test Content",
                UserID = 1,
                Groups = lgm
            };

            var isSave = sd.SaveStory(sm);
            
             var stories = sd.GetStories(sm.UserID);

             Assert.IsTrue(isSave);
             Assert.AreEqual(sm.Title, stories.Last().Title);
             Assert.AreEqual(sm.Description, stories.Last().Description);
             Assert.AreEqual(sm.StoryContent, stories.Last().StoryContent);

             var story = sd.GetStory(stories.Last().id.Value);
             Assert.AreEqual(sm.Title, story.Title);
             Assert.AreEqual(sm.Description, story.Description);
             Assert.AreEqual(sm.StoryContent, story.StoryContent);
        }

        [TestMethod]
        public void LoadGroups()
        {
            string groupName = "Rock";
            var groups = sd.GetGroupsInfo();

            var groupe = groups.Where(s => s.Name == groupName).FirstOrDefault();
            Assert.AreEqual(groupName ,groupe.Name);

        }
    }
}
