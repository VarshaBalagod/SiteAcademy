using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VideoBLL
    {
        VideoDAO daoVideo = new VideoDAO();

        public bool AddVideo(VideoDTO dtoVideo, SessionDTO session)
        {
            Video tblVideo= new Video();
            tblVideo.Title = dtoVideo.Title;
            tblVideo.VideoPath = dtoVideo.VideoPath;
            tblVideo.OriginalVideoPath = dtoVideo.OriginalVideoPath;
            tblVideo.AddDate = DateTime.Now;
            tblVideo.AddUserID = session.UserID;
            tblVideo.LastUpdatedUserID = session.UserID;
            tblVideo.LastUpdatedDate = DateTime.Now;
            int id = daoVideo.AddVideo(tblVideo);
            LogBLL.AddLog(General.ProcessType.VideoAdded, General.TableName.Video, id, session);
            return true;
        }

        public void DeleteVideo(int id, SessionDTO session)
        {
            daoVideo.DeleteVideo(id, session);
            LogBLL.AddLog(General.ProcessType.VideoDeleted, General.TableName.Video, id,session);
        }

        public List<VideoDTO> GetVideoList()
        {
            return daoVideo.GetVideoList();
        }

        public bool UpdateVideo(VideoDTO dtoVideo, SessionDTO session)
        {
            daoVideo.UpdateVideo(dtoVideo, session);
            LogBLL.AddLog(General.ProcessType.VideoUpdated, General.TableName.Video, dtoVideo.ID, session);
            return true;
        }
    }
}
