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
    public class PostController : BaseController
    {

        PostBLL bllPost = new PostBLL();

       
        public ActionResult PostList()
        {
            CountDTO dtoCount = new CountDTO();
            dtoCount = bllPost.GetAllCount();
            ViewData["AllCounts"] = dtoCount;

            List<PostDTO> postList = new List<PostDTO>();
            postList = bllPost.GetPost();
            return View(postList);
        }

        public ActionResult AddPost()
        {
            PostDTO dtoPost = new PostDTO();
            dtoPost.Categories = CategoryBLL.GetCategoryForDropdown();
            return View(dtoPost);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO dtoPost)
        {
            if (dtoPost.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach (var item in dtoPost.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".jpg" && ext != ".JPG" && ext != ".png" && ext != ".PNG"
                   && ext != ".jepg" && ext != ".JEPG" && ext != ".gif" && ext != ".GIF")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        dtoPost.Categories = CategoryBLL.GetCategoryForDropdown();
                        return View(dtoPost);
                    }
                }

                List<PostImageDTO> imageList = new List<PostImageDTO>();

                foreach (var postedfile in dtoPost.PostImage)
                {
                    Bitmap image = new Bitmap(postedfile.InputStream);
                    Bitmap resizeImage = new Bitmap(image, 750, 422);
                    string filename = "";
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imageList.Add(dto);
                }
                dtoPost.PostImages = imageList;

                string[] tags;
                tags = dtoPost.TagText.Split(',');
                List<PostTagDTO> tagsList = new List<PostTagDTO>();
                foreach (var item in tags)
                {
                    PostTagDTO tag = new PostTagDTO();
                    tag.TagContent = item;
                    tagsList.Add(tag);
                }
                dtoPost.TagList = tagsList;

                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bllPost.AddPost(dtoPost, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    dtoPost = new PostDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            dtoPost.Categories = CategoryBLL.GetCategoryForDropdown();
            return View(dtoPost);
        }

      

        public ActionResult UpdatePost(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = bllPost.GetPostWithID(ID);
            dto.Categories = CategoryBLL.GetCategoryForDropdown();
            dto.isUpdate = true;
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO dtoPost)
        {
            IEnumerable<SelectListItem> selectedlist = CategoryBLL.GetCategoryForDropdown();
            if (ModelState.IsValid)
            {
                if (dtoPost.PostImage[0] != null)
                {
                    foreach (var item in dtoPost.PostImage)
                    {
                        Bitmap image = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".jpg" && ext != ".JPG" && ext != ".png" && ext != ".PNG"
                       && ext != ".jepg" && ext != ".JEPG" && ext != ".gif" && ext != ".GIF")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            dtoPost.Categories = CategoryBLL.GetCategoryForDropdown();
                            return View(dtoPost);
                        }
                    }

                    List<PostImageDTO> imageList = new List<PostImageDTO>();

                    foreach (var postedfile in dtoPost.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.InputStream);
                        Bitmap resizeImage = new Bitmap(image, 750, 422);
                        string filename = "";
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedfile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + filename));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        imageList.Add(dto);
                    }
                    dtoPost.PostImages = imageList;
                    SessionDTO session = (SessionDTO)Session["UserInfo"];
                    if (bllPost.UpdatePost(dtoPost, session))
                    {
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.GeneralError;
                    }
                }
                else
                    ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            dtoPost = bllPost.GetPostWithID(dtoPost.PostID);
            dtoPost.Categories = selectedlist;
            dtoPost.isUpdate = true;
            return View(dtoPost);
        }

        public JsonResult DeletePostImage(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string imagePath = bllPost.DeletePostImage(id,session);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + imagePath));
                }
            }
            return Json("");
        }

        public JsonResult DeletePost(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            List<PostImageDTO> imageList = bllPost.DeletePost(id, session);
            if (imageList.Count != 0)
            {
                foreach (var image in imageList)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + image.ImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdminImages/PostImages/" + image.ImagePath));
                    }
                }
            }
            return Json("");
        }

        public JsonResult GetCounts()
        {
            CountDTO dtoCount = new CountDTO();
            dtoCount = bllPost.GetCounts();
            return Json(dtoCount, JsonRequestBehavior.AllowGet);
        }
    }
}