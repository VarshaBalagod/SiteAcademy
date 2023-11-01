using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserBLL bllUser = new UserBLL();

        // GET: Admin/User
        public ActionResult AddUser()
        {
            UserDTO dtoUser= new UserDTO();
            return View(dtoUser);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO dtoUser)
        {
            if (dtoUser.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = dtoUser.UserImage;
                Bitmap UserImageBt = new Bitmap(postedFile.InputStream);
                Bitmap resizeUserImage = new Bitmap(UserImageBt, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"
                    || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                {
                   
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    UserImageBt.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + filename));
                    dtoUser.ImagePath = filename;
                    SessionDTO session = (SessionDTO)Session["UserInfo"];
                    if (bllUser.AddUser(dtoUser, session))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        dtoUser = new UserDTO();
                        ModelState.Clear();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
                else
                    ViewBag.ProcessState = General.Messages.ExtensionError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoUser);
        }

        public ActionResult UserList()
        {
            List<UserDTO> users = new List<UserDTO>();
            users = bllUser.GetUserList();
            return View(users);
        }

        public ActionResult UpdateUser(int id)
        { 
            UserDTO dtoUser = new UserDTO();
            dtoUser = bllUser.GetUserWithID(id);
            return View(dtoUser);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDTO dtoUser)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (dtoUser.UserImage == null)
                {
                    //on post image get empty to avoid that again called get method with id .
                    //if user dont change image but change few data it should be update for that called update any changes in form if done.
                    if (dtoUser.ID != 0)
                    {
                        UserDTO image = new UserDTO();
                        image = bllUser.GetUserWithID(dtoUser.ID);
                        dtoUser.ImagePath = image.ImagePath;
                        bool change = false;
                        if (dtoUser.Username != image.Username)
                            change = true;
                        if (dtoUser.Password != image.Password)
                            change = true;
                        if (dtoUser.NameSurname != image.NameSurname)
                            change = true;
                        if (dtoUser.Email != image.Email)
                            change = true;
                        if (dtoUser.IsAdmin != image.IsAdmin)
                            change = true;
                        if (!change)
                        {
                            ViewBag.ProcessState = General.Messages.NochangeError;
                        }
                        else
                        {
                            bllUser.UpdateUser(dtoUser, session);
                            ViewBag.ProcessState = General.Messages.UpdateSuccess;
                        }
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ImageMissing;
                }
                else
                {
                    HttpPostedFileBase postedFile = dtoUser.UserImage;
                    Bitmap UserImageBt = new Bitmap(postedFile.InputStream);
                    Bitmap resizeUserImage = new Bitmap(UserImageBt, 128, 128);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"
                        || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                    {

                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFile.FileName;
                        UserImageBt.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + filename));
                        dtoUser.ImagePath = filename;
                        string oldImagePath = bllUser.UpdateUser(dtoUser, session);
                        if (dtoUser.UserImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + oldImagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + oldImagePath));
                            }
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoUser);
        }

        public JsonResult DeleteUser(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string imagePath = bllUser.DeleteUser(id, session);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/UserImages/" + imagePath));
                }
            }
            return Json("");
        }
    }
}