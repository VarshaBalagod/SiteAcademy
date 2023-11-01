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
using static System.Net.Mime.MediaTypeNames;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : BaseController
    {
        FavBLL bllFav = new FavBLL();

        public ActionResult UpdateFav()
        {
            FavDTO favDTO = new FavDTO();
            favDTO = bllFav.GetFav();
            return View(favDTO);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO dtoFav)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (dtoFav.FavImage != null && dtoFav.LogoImage != null)
                {
                    dtoFav.ID = 1;
                    bool blFav = false;
                    bool blLogo = false;
                    #region Faveimage saving
                    string favname = "";
                    HttpPostedFileBase postedfilefav = dtoFav.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizeFaveImage = new Bitmap(FavImage, 100, 100);
                    string extfav = Path.GetExtension(postedfilefav.FileName);
                    if (extfav == ".jpg" || extfav == ".JPG" || extfav == ".png" || extfav == ".PNG"
                    || extfav == ".jepg" || extfav == ".JEPG" || extfav == ".gif" || extfav == ".GIF")
                    {
                        string favUniqueNumber = Guid.NewGuid().ToString();
                        favname = favUniqueNumber + postedfilefav.FileName;
                        resizeFaveImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + favname));
                        dtoFav.Fav = favname;
                        blFav = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    #region logoimage saving
                    string logoname = "";
                    HttpPostedFileBase postedfileLogo = dtoFav.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfileLogo.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(LogoImage, 100, 100);
                    string extLogo = Path.GetExtension(postedfileLogo.FileName);
                    if (extLogo == ".jpg" || extLogo == ".JPG" || extLogo == ".png" || extLogo == ".PNG"
                    || extLogo == ".jepg" || extLogo == ".JEPG" || extLogo == ".gif" || extLogo == ".GIF")
                    {
                        string LogoUniqueNumber = Guid.NewGuid().ToString();
                        logoname = LogoUniqueNumber + postedfileLogo.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + logoname));
                        dtoFav.Logo = logoname;
                        blLogo = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    #region database entry
                    if (blFav && blLogo)
                    {
                        FavDTO returnDto = new FavDTO();
                        returnDto = bllFav.UpdateFav(dtoFav, session);
                        if (dtoFav.FavImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + returnDto.Fav)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + returnDto.Fav));
                            }
                        }
                        if (dtoFav.LogoImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + returnDto.Logo)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + returnDto.Logo));
                            }
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    #endregion
                }
                else
                {
                    //on post image get empty to avoid that again called get method with id .
                    //if user dont change image but change few data it should be update for that called update any changes in form if done.
                    if (dtoFav.ID != 0)
                    {
                        FavDTO image = new FavDTO();
                        image = bllFav.GetFav();
                        dtoFav.Fav = image.Fav;
                        dtoFav.Logo = image.Logo;
                        bool change = false;
                        if (dtoFav.Title != image.Title)
                            change = true;
                        if (!change)
                        {
                            ViewBag.ProcessState = General.Messages.NochangeError;
                        }
                        else
                        {
                            bllFav.UpdateFav(dtoFav, session);
                            ViewBag.ProcessState = General.Messages.UpdateSuccess;
                        }
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ImageMissing;
                }
            }
            else 
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }                   
            return View(dtoFav);
        }

        public ActionResult AddFav()
        {
            FavDTO dtoFav = new FavDTO();
            return View(dtoFav);
        }

        [HttpPost]
        public ActionResult AddFav(FavDTO dtoFav)
        {
            if (ModelState.IsValid)
            {
                if (dtoFav.FavImage != null && dtoFav.LogoImage != null)
                {
                    bool blFav = false;
                    bool blLogo = false;
                    #region Faveimage saving
                    string favname = "";
                    HttpPostedFileBase postedfilefav = dtoFav.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizeFaveImage = new Bitmap(FavImage, 100, 100);
                    string extfav = Path.GetExtension(postedfilefav.FileName);
                    if (extfav == ".jpg" || extfav == ".JPG" || extfav == ".png" || extfav == ".PNG"
                    || extfav == ".jepg" || extfav == ".JEPG" || extfav == ".gif" || extfav == ".GIF")
                    {
                        string favUniqueNumber = Guid.NewGuid().ToString();
                        favname = favUniqueNumber + postedfilefav.FileName;
                        resizeFaveImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + favname));
                        dtoFav.Fav = favname;
                        blFav = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    #region logoimage saving
                    string logoname = "";
                    HttpPostedFileBase postedfileLogo = dtoFav.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfileLogo.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(LogoImage, 100, 100);
                    string extLogo = Path.GetExtension(postedfileLogo.FileName);
                    if (extLogo == ".jpg" || extLogo == ".JPG" || extLogo == ".png" || extLogo == ".PNG"
                    || extLogo == ".jepg" || extLogo == ".JEPG" || extLogo == ".gif" || extLogo == ".GIF")
                    {
                        string LogoUniqueNumber = Guid.NewGuid().ToString();
                        logoname = LogoUniqueNumber + postedfileLogo.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + logoname));
                        dtoFav.Logo = logoname;
                        blLogo = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    SessionDTO session = (SessionDTO)Session["UserInfo"];
                    #region database entry
                    if(blFav && blLogo)
                    {
                        if (bllFav.AddFav(dtoFav, session))
                        {
                            ViewBag.ProcessState = General.Messages.AddSuccess;
                            ModelState.Clear();
                            dtoFav = new FavDTO();
                        }
                        else
                        {
                            ViewBag.ProcessState = General.Messages.GeneralError;
                        }
                    }
                    #endregion
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ImageMissing;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoFav);
        }

        public ActionResult FavList()
        {
            List<FavDTO> favlist =  new List<FavDTO>();
            favlist = bllFav.GetFavList();
            return View(favlist);
        }

        public ActionResult UpdateFavWithID(int id)
        { 
            FavDTO dto = new FavDTO();
            dto = bllFav.GetFavWithID(id);
            return View(dto); 
        }

        [HttpPost]
        public ActionResult UpdateFavWithID(FavDTO dtoFav)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (dtoFav.FavImage != null && dtoFav.LogoImage != null)
                {
                    bool blFav = false;
                    bool blLogo = false;
                    #region Faveimage saving
                    string favname = "";
                    HttpPostedFileBase postedfilefav = dtoFav.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizeFaveImage = new Bitmap(FavImage, 100, 100);
                    string extfav = Path.GetExtension(postedfilefav.FileName);
                    if (extfav == ".jpg" || extfav == ".JPG" || extfav == ".png" || extfav == ".PNG"
                    || extfav == ".jepg" || extfav == ".JEPG" || extfav == ".gif" || extfav == ".GIF")
                    {
                        string favUniqueNumber = Guid.NewGuid().ToString();
                        favname = favUniqueNumber + postedfilefav.FileName;
                        resizeFaveImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + favname));
                        dtoFav.Fav = favname;
                        blFav = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    #region logoimage saving
                    string logoname = "";
                    HttpPostedFileBase postedfileLogo = dtoFav.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfileLogo.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(LogoImage, 100, 100);
                    string extLogo = Path.GetExtension(postedfileLogo.FileName);
                    if (extLogo == ".jpg" || extLogo == ".JPG" || extLogo == ".png" || extLogo == ".PNG"
                    || extLogo == ".jepg" || extLogo == ".JEPG" || extLogo == ".gif" || extLogo == ".GIF")
                    {
                        string LogoUniqueNumber = Guid.NewGuid().ToString();
                        logoname = LogoUniqueNumber + postedfileLogo.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + logoname));
                        dtoFav.Logo = logoname;
                        blLogo = true;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                    #endregion

                    #region database entry
                    if (blFav && blLogo)
                    {
                        FavDTO returnDto = new FavDTO();
                        returnDto = bllFav.UpdateFavWithID(dtoFav, session);
                        if (dtoFav.FavImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + returnDto.Fav)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/FavImages/" + returnDto.Fav));
                            }
                        }
                        if (dtoFav.LogoImage != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + returnDto.Logo)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/LogoImages/" + returnDto.Logo));
                            }
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    #endregion
                }
                else
                {
                    //on post image get empty to avoid that again called get method with id .
                    //if user dont change image but change few data it should be update for that called update any changes in form if done.
                    if (dtoFav.ID != 0)
                    {
                        FavDTO image = new FavDTO();
                        image = bllFav.GetFav();
                        dtoFav.Fav = image.Fav;
                        dtoFav.Logo = image.Logo;
                        bool change = false;
                        if (dtoFav.Title != image.Title)
                            change = true;
                        if (!change)
                        {
                            ViewBag.ProcessState = General.Messages.NochangeError;
                        }
                        else
                        {
                            bllFav.UpdateFavWithID(dtoFav, session);
                            ViewBag.ProcessState = General.Messages.UpdateSuccess;
                        }
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ImageMissing;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoFav);
        }
    }
}