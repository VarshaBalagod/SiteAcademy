using BLL;
using DAL;
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
    public class AdsController : BaseController
    {
        AdsBLL bllAds = new AdsBLL();

        // GET: Admin/Ads
        public ActionResult AddAds()
        {
            AdsDTO ads = new AdsDTO();
            return View(ads);
        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO ads)
        {
            if (ads.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = ads.AdsImage;
                Bitmap AdsImageBt = new Bitmap(postedFile.InputStream);
                Bitmap resizeUserImage = new Bitmap(AdsImageBt, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"
                    || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                {

                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    AdsImageBt.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + filename));
                    ads.ImagePath = filename;
                    SessionDTO session = (SessionDTO)Session["UserInfo"];
                    if (bllAds.AddAds(ads,session))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        ModelState.Clear();
                        ads = new AdsDTO();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
                else
                    ViewBag.ProcessState = General.Messages.ExtensionError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
           
            return View(ads);
        }

        public ActionResult AdsList()
        {
            List<AdsDTO> adsList= new List<AdsDTO>();
            adsList = bllAds.GetAdsList();
            return View(adsList);
        }

        public ActionResult UpdateAds(int id)
        {
            AdsDTO ads = new AdsDTO();
            ads = bllAds.GetAdsWithID(id);
            return View(ads);           
        }

        [HttpPost]
        public ActionResult UpdateAds(AdsDTO ads)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (ads.AdsImage == null)
                {
                    //on post image get empty to avoid that again called get method with id .
                    //if user dont change image but change few data it should be update for that called update any changes in form if done.
                    if (ads.ID != 0)
                    {
                        AdsDTO image = new AdsDTO();
                        image = bllAds.GetAdsWithID(ads.ID);
                        ads.ImagePath = image.ImagePath;
                        bool change=false;
                        if (ads.Name != image.Name)
                            change = true;
                        if (ads.Link != image.Link)
                            change = true;
                        if (ads.Size != image.Size)
                            change = true;
                        if (!change)
                        {
                            ViewBag.ProcessState = General.Messages.NochangeError;
                        }
                        else
                        {
                            bllAds.UpdateAds(ads, session);
                            ViewBag.ProcessState = General.Messages.UpdateSuccess;
                        }                      
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ImageMissing;
                }
                else
                {
                    HttpPostedFileBase postedFile = ads.AdsImage;
                    Bitmap AdsImageBt = new Bitmap(postedFile.InputStream);
                    Bitmap resizeUserImage = new Bitmap(AdsImageBt, 128, 128);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"
                        || ext == ".jepg" || ext == ".JEPG" || ext == ".gif" || ext == ".GIF")
                    {

                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFile.FileName;
                        AdsImageBt.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + filename));
                        ads.ImagePath = filename;
                        string oldImagePath = bllAds.UpdateAds(ads, session);
                        if (ads.AdsImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + oldImagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + oldImagePath));
                            }
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;

                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }              
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;

            return View(ads);
        }

        public JsonResult DeleteAds(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string imagePath = bllAds.DeleteAds(id, session);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/AdsImages/" + imagePath));
                }
            }
            return Json("");
        }
    }
}