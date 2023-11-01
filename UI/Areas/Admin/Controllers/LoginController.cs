using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.Web.Security;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userBLL = new UserBLL();

        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO userDTO = new UserDTO();
            return View(userDTO);
        }

        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (model.Username != null && model.Password != null)
            {
                UserDTO user = userBLL.GetUserWithUsernameAndPassword(model);
                if (user.ID != 0)
                {
                    SessionDTO session = new SessionDTO();
                    session.UserID = user.ID;
                    session.IsAdmin = user.IsAdmin;
                    session.ImagePath = user.ImagePath;
                    session.NameSurname = user.NameSurname;

                    HttpCookie cookie = new HttpCookie("Id");
                    cookie.Value = user.ID.ToString();
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(),false);

                    Session.Add("UserInfo", session);
                    Session.Add("ID", user.ID);

                    //UserStatic.UserID = user.ID;
                    //UserStatic.IsAdmin = user.IsAdmin;
                    //UserStatic.ImagePath = user.ImagePath;
                    //UserStatic.NameSurname = user.NameSurname;

                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12, session);
                    return RedirectToAction("PostList", "Post");
                }
                else
                    return View(model);
            }
            else
                return View(model);
        }
    }
}