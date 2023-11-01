using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        PostBLL bllPost=new PostBLL();

    
        public ActionResult UnapprovedComments()
        { 
            List<CommentDTO> commentsList = new List<CommentDTO>();
            commentsList = bllPost.GetComments();
            return View(commentsList);
        }

        public ActionResult ApprovedComment(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllPost.ApprovedComment(ID, session);
            return RedirectToAction("UnapprovedComments", "Comment");
        }

        public ActionResult AllComments()
        {
            List<CommentDTO> listComment = new List<CommentDTO>();
            listComment = bllPost.GetAllComments();
            return View(listComment);
        }

        public ActionResult ApprovedCommentInList(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllPost.ApprovedComment(ID, session);
            return RedirectToAction("AllComments", "Comment");
        }

        public JsonResult DeleteComment(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllPost.DeleteComment(ID, session);
            return Json("");
        }
    }
}