using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public int AddFav(FavLogoTitle tblFav)
        {
            try
            {
                db.FavLogoTitles.Add(tblFav);
                db.SaveChanges();
                return tblFav.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavDTO GetFav()
        {
            try
            {
                FavLogoTitle tblFav = db.FavLogoTitles.First(x => x.ID == 1);
                FavDTO d = new FavDTO();
                d.ID = tblFav.ID;
                d.Title = tblFav.Title;
                d.Fav = tblFav.Fav;
                d.Logo = tblFav.Logo;
                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FavDTO> GetFavList()
        {
            try
            {
                List<FavDTO> dtoFav = new List<FavDTO>();
                List<FavLogoTitle> tblFav = db.FavLogoTitles.Where(x => x.IsDeleted == false).ToList();
                foreach(var item in tblFav)
                {
                    FavDTO dTO = new FavDTO();
                    dTO.ID = item.ID;
                    dTO.Title = item.Title;
                    dTO.Fav = item.Fav;
                    dTO.Logo = item.Logo;
                    dtoFav.Add(dTO);
                }
                return dtoFav;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavDTO GetFavWithID(int id)
        {
            try
            {
                FavDTO dtoFav = new FavDTO();
                FavLogoTitle tblFav = db.FavLogoTitles.First(x=>x.ID == id);
                dtoFav.ID = tblFav.ID;
                dtoFav.Title = tblFav.Title;
                dtoFav.Logo = tblFav.Logo;
                dtoFav.Fav = tblFav.Fav;
                return dtoFav;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavDTO UpdateFav(FavDTO dtoFav,SessionDTO session)
        {
            try
            {
                FavLogoTitle tblFav = db.FavLogoTitles.First(x => x.ID == 1);
                FavDTO rtFav = new FavDTO();
                rtFav.ID = tblFav.ID;
                rtFav.Title = tblFav.Title;
                rtFav.Fav = tblFav.Fav;
                rtFav.Logo = tblFav.Logo;

                tblFav.Title = dtoFav.Title;
                if (dtoFav.Fav != null)
                    tblFav.Fav = dtoFav.Fav;
                if (dtoFav.Logo != null)
                    tblFav.Logo = dtoFav.Logo;
                tblFav.LastUpdatedUserID = session.UserID;
                tblFav.LastUpdeatedDate = DateTime.Now;
                db.SaveChanges();
                return rtFav;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavDTO UpdateFavWithID(FavDTO dtoFav, SessionDTO session)
        {
            try
            {
                FavLogoTitle tblFav = db.FavLogoTitles.First(x => x.ID == dtoFav.ID);
                FavDTO rtFav = new FavDTO();
                rtFav.ID = tblFav.ID;
                rtFav.Title = tblFav.Title;
                rtFav.Fav = tblFav.Fav;
                rtFav.Logo = tblFav.Logo;

                tblFav.Title = dtoFav.Title;
                if (dtoFav.Fav != null)
                    tblFav.Fav = dtoFav.Fav;
                if (dtoFav.Logo != null)
                    tblFav.Logo = dtoFav.Logo;
                tblFav.LastUpdatedUserID = session.UserID;
                tblFav.LastUpdeatedDate = DateTime.Now;
                db.SaveChanges();
                return rtFav;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
