using StortyBack.Data;
using StorySocialNetwork.Models;
using StorySocialNetwork.StorySNData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StorySocialNetwork.Controllers
{
    public class StoryController : Controller
    {
        //
        // GET: /Story/

        StoryDataConvert convert = new StoryDataConvert();
        
        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult Login(LoginModel lm)
        {
            UserData ud = new UserData();
            ud = convert.UserModelToData(lm);
            var userId = ud.CheckLogin(ud);
            if (userId > 0)
            {
                FormsAuthentication.SetAuthCookie(userId.ToString(), false);
                return RedirectToAction("Stories");
            }
            else
            {
                return RedirectToAction("LoginPage");
            }

        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPage");

        }

        [Authorize]
        public ActionResult Stories()
        {
            StoryData sd = new StoryData();
            var userID = Convert.ToInt32(User.Identity.Name);
            List<StoryModel> stories =   new List<StoryModel>();
            List<StoryData>  sds = sd.GetStories(userID);
            foreach (var item in sds)
            {
                stories.Add(convert.StoryDataToModel(item));
            }

            return View(stories);
        }

        [Authorize]
        public ActionResult Groups()
        {
            GroupData gd = new GroupData();
            var gds = gd.GetGroupsInfo();
            List<GroupModel> grModel = new List<GroupModel>();
            foreach (var item in gds)
            {
                grModel.Add(convert.GroupDataToModel(item));
            }

            return View(grModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetStory(int id)
        {
            StoryData sd = new StoryData();
            StoryModel sm = new StoryModel();
            sm = convert.StoryDataToModel(sd.GetStory(id));

            return PartialView("Story",sm);
        }

        [Authorize]
        public ActionResult AddEditStory(int? id)
        {
            StoryData sd = new StoryData();
            StoryModel sm = new StoryModel();
            if (id != null)
            {
                sm = convert.StoryDataToModel( sd.GetStory(id.Value));
            }

            GroupData gd = new GroupData();
            var gds = gd.GetGroups(id);
            List<GroupModel> grModel = new List<GroupModel>();
            foreach (var item in gds)
            {
                grModel.Add(convert.GroupDataToModel(item));
            }

            sm.Groups = grModel;

            return View(sm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult StorySave(StoryModel model)
        {
            model.UserID = Convert.ToInt32(User.Identity.Name);
            StoryData sd = new StoryData();
            sd = convert.StoryModelToData(model);
            var result = sd.SaveStory(sd);

            string action ;
            if (result)
            {
                 action = "Stories";
            }
            else
            {
                 action = "AddEditStory"; 
            }

            return RedirectToAction(action);
        }
    }
}
