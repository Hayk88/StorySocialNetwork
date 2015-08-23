using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorySocialNetwork;
using StorySocialNetwork.StorySNData;
using StorySocialNetwork.Models;
using System.Linq;
using System.Collections.Generic;
using StortyBack.Data;

namespace UnitTestForStorySN
{
    [TestClass]
    public class StorySNTest
    {
        StoryDataConvert sdc = new StoryDataConvert();
        StoryData sd = new StoryData();
        GroupData gd = new GroupData();
        UserData ud = new UserData();
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
            UserData lm = new UserData()
            {
                UserName = "hayk",
                Password= "hayk"
            };
            var userID = ud.CheckLogin(lm);
            Assert.AreEqual(userid, userID);
        }

        [TestMethod]
        public void LoadSaveStory()
        {
            GroupData groupe = new GroupData
            {
                id = 1,
                isSelected = true
            };
            List<GroupData> lgm = new List<GroupData>();
            lgm.Add(groupe);
            StoryData sm = new StoryData()
            { 
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
            var groups = gd.GetGroupsInfo();

            var groupe = groups.Where(s => s.Name == groupName).FirstOrDefault();
            Assert.AreEqual(groupName ,groupe.Name);

        }
    }
}
