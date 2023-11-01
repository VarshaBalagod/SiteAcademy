using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video tblVideo)
        {
            try
            {
                db.Videos.Add(tblVideo);
                db.SaveChanges();
                return tblVideo.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVideo(int id, SessionDTO session)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == id);
                video.IsDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdatedDate = DateTime.Now;
                video.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<VideoDTO> GetVideoList()
        {
            try
            {
                List<VideoDTO> dtoList= new List<VideoDTO>();
                List<Video> tblList = db.Videos.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in tblList)
                {   
                    VideoDTO dto = new VideoDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dtoList.Add(dto);                    
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVideo(VideoDTO dtoVideo, SessionDTO session)
        {
            try
            {
                Video tblVideo = db.Videos.First(x=>x.ID == dtoVideo.ID);
                tblVideo.ID = dtoVideo.ID;
                tblVideo.Title = dtoVideo.Title;
                tblVideo.VideoPath = dtoVideo.VideoPath;
                tblVideo.OriginalVideoPath= dtoVideo.OriginalVideoPath;
                tblVideo.LastUpdatedUserID = session.UserID;
                tblVideo.LastUpdatedDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
