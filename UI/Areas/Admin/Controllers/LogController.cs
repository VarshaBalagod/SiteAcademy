using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LogController : BaseController
    {
        LogBLL bllLog = new LogBLL();

        public ActionResult LogList()
        {
            List<LogDTO> dtoLogo = new List<LogDTO>();
            dtoLogo = bllLog.GetLogs();
            return View(dtoLogo);
        }
    }
}