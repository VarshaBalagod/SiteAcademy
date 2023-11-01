using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactBLL bllContact = new ContactBLL();

        public ActionResult UnreadMessages()
        {
            List<ContactDTO> dtocontacts = new List<ContactDTO>();
            dtocontacts = bllContact.GetUnreadMessages();
            return View(dtocontacts);
        }

        public ActionResult AllMessages()
        {
            List<ContactDTO> dtoContact = new List<ContactDTO>();
            dtoContact = bllContact.GetAllMessages();
            return View(dtoContact);
        }

        public ActionResult ReadMessage(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllContact.ReadMessage(ID, session);
            return RedirectToAction("UnreadMessages");
        }

        public ActionResult ReadMessage2(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllContact.ReadMessage(ID, session);
            return RedirectToAction("AllMessages");
        }

        public JsonResult DeleteContact(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllContact.DeleteMessage(ID, session);
            return Json("");
        }
    }
}