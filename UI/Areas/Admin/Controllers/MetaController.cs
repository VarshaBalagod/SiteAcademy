using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        MetaBLL bllmeta = new MetaBLL();
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeta()
        {
            MetaDTO metadto = new MetaDTO();
            return View(metadto);
        }

        [HttpPost]
        public ActionResult AddMeta(MetaDTO model) 
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bllmeta.AddMeta(model, session)) 
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
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
            MetaDTO newmodel = new MetaDTO();
            return View(newmodel);
        }

        public ActionResult MetaList()
        {
            List<MetaDTO> metaTable= new List<MetaDTO>();
            metaTable = bllmeta.GetMetaData();
            return View(metaTable);
        }

        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO dtoMeta = new MetaDTO();
            dtoMeta = bllmeta.GetMetaWithID(ID);
            return View(dtoMeta);
        }

        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO dtoMeta)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bllmeta.UpdateMeta(dtoMeta, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
                }
                else
                    ViewBag.ProcessState=General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoMeta);
        }

        public JsonResult DeleteMeta(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllmeta.DeleteMeta(ID, session);
            return Json("");
        }
    }
}