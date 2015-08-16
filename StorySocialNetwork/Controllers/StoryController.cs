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

        StoryData sd = new StoryData();
       
        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult Login(LoginModel lm)
        {
            var userId = sd.CheckLogin(lm);
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
            var userID = Convert.ToInt32(User.Identity.Name);
            List<StoryModel> stories = sd.GetStories(userID);
            return View(stories);
        }

        [Authorize]
        public ActionResult Groups()
        {
            var grModel = sd.GetGroupsInfo();
            return View(grModel);
        }

        [Authorize]
        public ActionResult Story(int id)
        {
            StoryModel sm = new StoryModel();
            sm = sd.GetStory(id);

            return View(sm);
        }

        [Authorize]
        public ActionResult AddEditStory(int? id)
        {
            StoryModel sm = new StoryModel();
            if (id != null)
            {
                sm = sd.GetStory(id.Value);
            }
            sm.Groups = sd.GetGroups(id);
            return View(sm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult StorySave(StoryModel model)
        {
            model.UserID = Convert.ToInt32(User.Identity.Name); ;
            var result = sd.SaveStory(model);

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
