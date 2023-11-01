using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO daoSocialMedia = new SocialMediaDAO();

        public bool AddSocialMedia(SocialMediaDTO dtoSM, SessionDTO sesssion)
        {
            SocialMedia socialMedia = new SocialMedia();
            socialMedia.Name = dtoSM.Name;
            socialMedia.Link = dtoSM.Link;
            socialMedia.ImagePath = dtoSM.ImagePath;
            socialMedia.AddDate = DateTime.Now;
            socialMedia.LastUpdatedDate = DateTime.Now;
            socialMedia.LastUpdatedUserID = sesssion.UserID;
            int ID = daoSocialMedia.AddSocialMedia(socialMedia);
            LogBLL.AddLog(General.ProcessType.SocialMediaAdded, General.TableName.SocialMedia, ID, sesssion);
            return true;
        }

        public string DeleteSocialMedia(int id,SessionDTO session)
        {
            string imagePath = daoSocialMedia.DeleteSocialMedia(id, session);
            LogBLL.AddLog(General.ProcessType.SocialMediaDeleted, General.TableName.SocialMedia, id, session);
            return imagePath;
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {
            List<SocialMediaDTO> dtoList= new List<SocialMediaDTO>();
            dtoList = daoSocialMedia.GetSocialMedia();
            return dtoList;
        }

        public SocialMediaDTO GetSocialMediaWithID(int id)
        {
            SocialMediaDTO dtoSM =daoSocialMedia.GetSocialMediaWithID(id);
            return dtoSM;
        }

        public string UpdateSocialMedia(SocialMediaDTO dtoSM, SessionDTO session)
        {
            string oldImagePath = daoSocialMedia.UpdateSocialMedia(dtoSM, session);
            LogDAO.AddLog(General.ProcessType.SocialMediaUpdated, General.TableName.SocialMedia, dtoSM.ID, session);
            return oldImagePath;
        }
    }
}
