using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseController
    {
        SocialMediaBLL bllSocialMedia = new SocialMediaBLL();

        // GET: Admin/SocialMedia
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO dtoSM = new SocialMediaDTO();
            return View(dtoSM);
        }

        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO dtoSM)
        {
            if (dtoSM.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = dtoSM.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" 
                    || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                {
                    
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + filename));
                    dtoSM.ImagePath = filename;
                    SessionDTO session = (SessionDTO)Session["UserInfo"];
                    if(bllSocialMedia.AddSocialMedia(dtoSM, session))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        dtoSM = new SocialMediaDTO();
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
            return View(dtoSM);
        }

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = bllSocialMedia.GetSocialMedia();
            return View(dtoList);
        }

        public ActionResult UpdateSocialMedia(int id)
        {
            SocialMediaDTO dtoSM = bllSocialMedia.GetSocialMediaWithID(id);
            return View(dtoSM);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO dtoSM)
        {
            if (ModelState.IsValid) 
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (dtoSM.SocialImage == null)
                {
                    //on post image get empty to avoid that again called get method with id .
                    //if user dont change image but change few data it should be update for that called update any changes in form if done.
                    if (dtoSM.ID != 0)
                    {
                        SocialMediaDTO image = new SocialMediaDTO();
                        image = bllSocialMedia.GetSocialMediaWithID(dtoSM.ID);
                        dtoSM.ImagePath = image.ImagePath;
                        bool change = false;
                        if (dtoSM.Name != image.Name)
                            change = true;
                        if (dtoSM.Link != image.Link)
                            change = true;
                        if (!change)
                        {
                            ViewBag.ProcessState = General.Messages.NochangeError;
                        }
                        else
                        {
                            bllSocialMedia.UpdateSocialMedia(dtoSM, session);
                            ViewBag.ProcessState = General.Messages.UpdateSuccess;
                        }
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ImageMissing;
                }
                else
                {
                    HttpPostedFileBase postedFile = dtoSM.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"
                        || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFile.FileName;
                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + filename));
                        dtoSM.ImagePath = filename;
                        string oldImagePath = bllSocialMedia.UpdateSocialMedia(dtoSM, session);
                        if (dtoSM.SocialImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + oldImagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + oldImagePath));
                            }
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoSM);
        }

        public JsonResult DeleteSocialMedia(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string imagePath = bllSocialMedia.DeleteSocialMedia(id, session);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/SocialMediaImages/" + imagePath));
                }
            }
            return Json("");
        }
    }
}