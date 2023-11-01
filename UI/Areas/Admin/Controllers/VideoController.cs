using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bllVideo = new VideoBLL();

        public ActionResult AddVideo()
        {
            VideoDTO dtoVideo = new VideoDTO();
            return View(dtoVideo);
        }

        [HttpPost]
        public ActionResult AddVideo(VideoDTO dtoVideo)
        {
            if(ModelState.IsValid)
            {
                //<iframe width="560" height="315" src="https://www.youtube.com/embed/bJzb-RuUcMU" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
                string path = dtoVideo.OriginalVideoPath.Substring(32);
                string mergerlink = "https://www.youtube.com/embed/";
                mergerlink += path;
                dtoVideo.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}""  frameborder=""0"" allowfullscreen></iframe>", mergerlink);
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if(bllVideo.AddVideo(dtoVideo, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    dtoVideo = new VideoDTO();
                }
                else
                {
                    ViewBag.ProcesssState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoVideo);
        }

        public ActionResult VideoList()
        {
            List<VideoDTO> dtoList= new List<VideoDTO>();
            dtoList = bllVideo.GetVideoList();
            return View(dtoList);
        }

        public ActionResult UpdateVideo(int  id)
        {
            List<VideoDTO> dtoList = new List<VideoDTO>();
            dtoList = bllVideo.GetVideoList();
            VideoDTO dtoVideo = new VideoDTO();
            dtoVideo = dtoList.First(x => x.ID == id);
            return View(dtoVideo);
        }

        [HttpPost]
        public ActionResult UpdateVideo(VideoDTO dtoVideo)
        { 
            if(ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                string path = dtoVideo.OriginalVideoPath.Substring(32);
                string mergerlink = "https://www.youtube.com/embed/";
                mergerlink += path;
                dtoVideo.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}""  frameborder=""0"" allowfullscreen></iframe>", mergerlink);

                if (bllVideo.UpdateVideo(dtoVideo, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    dtoVideo = new VideoDTO();
                }
                else
                {
                    ViewBag.ProcesssState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(dtoVideo);
        }

        public JsonResult DeleteVideo(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bllVideo.DeleteVideo(id, session);
            return Json("");
        }
    }
}