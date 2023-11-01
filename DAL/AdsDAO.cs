using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ad tblAd)
        {
            try
            {
                db.Ads.Add(tblAd);
                db.SaveChanges();
                return tblAd.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteAds(int id, SessionDTO session)
        {
            try
            {
                Ad tblAd = db.Ads.First(x => x.ID == id);
                string imagePath = tblAd.ImagePath;
                tblAd.DeletedDate = DateTime.Now;
                tblAd.IsDeleted = true;
                tblAd.LastUpdatedDate = DateTime.Now;
                tblAd.LastUpdatedUserID = session.UserID;
                db.SaveChanges();           
                return imagePath;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AdsDTO> GetAdsList()
        {
            try
            {
                List<Ad> adsTlsit = db.Ads.Where(x => x.IsDeleted == false).ToList();
                List<AdsDTO> adsDTOs = new List<AdsDTO>();
                foreach (var item in adsTlsit) 
                {
                    AdsDTO dTO = new AdsDTO();
                    dTO.ID = item.ID;
                    dTO.Name = item.Name;
                    dTO.ImagePath = item.ImagePath;
                    dTO.Link=item.Link;
                    dTO.Size = item.Size;
                    adsDTOs.Add(dTO);
                }
                return adsDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdsDTO GetAdsWithID(int id)
        {
           try
            {
                AdsDTO ads = new AdsDTO();
                Ad tblAd = db.Ads.First(x => x.ID == id);
                ads.ID = tblAd.ID;
                ads.Name = tblAd.Name;
                ads.ImagePath = tblAd.ImagePath;
                ads.Link = tblAd.Link;
                ads.Size = tblAd.Size;
                return ads;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateAds(AdsDTO ads,SessionDTO session)
        {
            try
            {
                Ad tblAd = db.Ads.First(x => x.ID == ads.ID);
                string oldImagePath = tblAd.ImagePath;
                tblAd.Name = ads.Name;
                tblAd.Link = ads.Link;
                tblAd.Size = ads.Size;
                if (ads.ImagePath != null)
                    tblAd.ImagePath = ads.ImagePath;
                tblAd.LastUpdatedDate = DateTime.Now;
                tblAd.LastUpdatedUserID = session.UserID;
                return oldImagePath;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
