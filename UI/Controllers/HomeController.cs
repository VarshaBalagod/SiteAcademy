using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL bllLayout = new LayoutBLL();
        GeneralBLL bllGeneral = new GeneralBLL();
        PostBLL bllPost = new PostBLL();
        ContactBLL bllContact = new ContactBLL();   
      

        // GET: Default
        public ActionResult Index()
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;

            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral = bllGeneral.GetAllPosts();
            return View(dtoGeneral);
        }

        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;

            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral = bllGeneral.GetCategoryPostList(CategoryName);

            return View(dtoGeneral);
        }

        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;
            
            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral = bllGeneral.GetPostDetailPageItemWithID(ID);
           
            return View(dtoGeneral);
        }
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO dtoGeneral)
        {
            if (dtoGeneral.Name != null && dtoGeneral.Email != null && dtoGeneral.Message != null)
            {
                if(bllPost.AddComment(dtoGeneral))
                {
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }

            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;
          
            dtoGeneral = bllGeneral.GetPostDetailPageItemWithID(dtoGeneral.PostID);

            return View(dtoGeneral);
        }
        [Route("contactus")]
        public ActionResult ContactList()
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;

            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral = bllGeneral.GetContactPageItem();
            return View(dtoGeneral);
        }
        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactList(GeneralDTO dtoGen)
        {
            if (dtoGen.Name != null && dtoGen.Subject != null && dtoGen.Email != null && dtoGen.Message != null)
            {
                if(bllContact.AddContact(dtoGen))
                {
                    ViewData["CommentState"] = "Success";
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }            
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;

            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral = bllGeneral.GetContactPageItem();
            return View(dtoGeneral);
        }
        [Route("search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO dtoGeneral)
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout = bllLayout.GetLayoutData();
            ViewData["LayoutDTO"] = dtoLayout;

            GeneralDTO dtoGen = new GeneralDTO();
            dtoGen = bllGeneral.GetSearchPost(dtoGeneral.SearchText);
            return View(dtoGen); 
        }
    }
}