using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        AddressBLL bllAddress = new AddressBLL();

        public ActionResult AddAddress()
        {
            AddressDTO dtoAddress = new AddressDTO();
            return View(dtoAddress);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO dtoAddress)
        {
            if(ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];

                if (bllAddress.AddAddress(dtoAddress,session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    dtoAddress = new AddressDTO();
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

            return View(dtoAddress);
        }

        public ActionResult AddressList() 
        {
            List<AddressDTO> dtoAddress = new List<AddressDTO>();
            dtoAddress = bllAddress.GetAddressList();
            return View(dtoAddress);
        }

        public ActionResult UpdateAddress(int id)
        {
            AddressDTO addressDTO = new AddressDTO();
            addressDTO = bllAddress.GetAddressWithID(id);
            return View(addressDTO);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO addressDTO)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bllAddress.UpdateAddress(addressDTO, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess; 
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState=General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(addressDTO);
        }

        public JsonResult DeleteAddress(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllAddress.DeleteAddress(id, session);
            return Json("");
        }
    }
}