using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {       
        CategoryBLL bllCategory = new CategoryBLL();

        public ActionResult AddCategory()
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            return View(categoryDTO);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if(bllCategory.AddCategory(categoryDTO, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    categoryDTO = new CategoryDTO();
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
            return View(categoryDTO);
        }

        public ActionResult CategoryList()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            categories = bllCategory.GetCategoryList();
            return View(categories);
        }

        public ActionResult UpdateCategory(int id)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            categories = bllCategory.GetCategoryList();
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO = categories.First(x => x.ID == id);
            return View(categoryDTO);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bllCategory.UpdateCategory(categoryDTO, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
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
            return View(categoryDTO);
        }

        public JsonResult DeleteCategory(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            List<PostImageDTO> imageList = bllCategory.DeleteCategory(id, session);
            if (imageList.Count > 0)
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
    }
}