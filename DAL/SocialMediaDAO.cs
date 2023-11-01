using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia socialMedia)
        {
            try
            {
                db.SocialMedias.Add(socialMedia);
                db.SaveChanges();
                return socialMedia.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteSocialMedia(int id, SessionDTO session)
        {
            try
            {
                SocialMedia tblSM= db.SocialMedias.First(s => s.ID == id);
                string imagePath=tblSM.ImagePath;
                tblSM.IsDeleted = true;
                tblSM.DeletedDate = DateTime.Now;
                tblSM.LastUpdatedDate = DateTime.Now;
                tblSM.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
                return imagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {
            try
            {
                List<SocialMedia> list = db.SocialMedias.Where(x => x.IsDeleted == false).ToList();
                List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
                foreach(var item in list)
                {
                    SocialMediaDTO socialMedia = new SocialMediaDTO();
                    socialMedia.ID = item.ID;
                    socialMedia.Name = item.Name;
                    socialMedia.ImagePath = item.ImagePath;
                    socialMedia.Link = item.Link;
                    dtoList.Add(socialMedia);
                }
                return dtoList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SocialMediaDTO GetSocialMediaWithID(int id)
        {
            try
            {
                SocialMedia tableSM = db.SocialMedias.First(x => x.ID == id);
                SocialMediaDTO dtoSM = new SocialMediaDTO();
                dtoSM.ID = tableSM.ID;
                dtoSM.Name = tableSM.Name;
                dtoSM.Link = tableSM.Link;
                dtoSM.ImagePath = tableSM.ImagePath;
                return dtoSM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateSocialMedia(SocialMediaDTO dtoSM, SessionDTO session)
        {
            try
            {
                SocialMedia tableSM = db.SocialMedias.First(x => x.ID == dtoSM.ID);
                string oldImagePath = tableSM.ImagePath;
                tableSM.Name = dtoSM.Name;
                tableSM.Link = dtoSM.Link;
                if (dtoSM.ImagePath != null)
                    tableSM.ImagePath = dtoSM.ImagePath;
                tableSM.LastUpdatedDate = DateTime.Now;
                tableSM.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
