using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO daoAds = new AdsDAO();

        public bool AddAds(AdsDTO ads, SessionDTO session)
        {
            Ad tblAd = new Ad();
            tblAd.Name = ads.Name;
            tblAd.ImagePath = ads.ImagePath;
            tblAd.Link = ads.Link;
            tblAd.Size = ads.Size;
            tblAd.AddDate = DateTime.Now;
            tblAd.LastUpdatedUserID = session.UserID;
            tblAd.LastUpdatedDate = DateTime.Now;
            int id = daoAds.AddAds(tblAd);
            LogBLL.AddLog(General.ProcessType.AdsAdded, General.TableName.Ads, id, session);
            return true;
        }

        public string DeleteAds(int id,SessionDTO session)
        {
            string imagePath = daoAds.DeleteAds(id, session);
            LogBLL.AddLog(General.ProcessType.AdsDeleted, General.TableName.Ads, id, session);
            return imagePath;
        }

        public List<AdsDTO> GetAdsList()
        {
            return daoAds.GetAdsList();
        }

        public AdsDTO GetAdsWithID(int id)
        {
            return daoAds.GetAdsWithID(id); 
        }

        public string UpdateAds(AdsDTO ads, SessionDTO session)
        {
            string oldImagePath = daoAds.UpdateAds(ads, session);
            LogBLL.AddLog(General.ProcessType.AdsUpdated, General.TableName.Ads, ads.ID, session);
            return oldImagePath;
        }
    }
}
