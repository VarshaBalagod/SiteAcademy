using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FavBLL
    {
        FavDAO daoFav= new FavDAO();

        public bool AddFav(FavDTO dtoFav, SessionDTO session)
        {
            FavLogoTitle tblFav = new FavLogoTitle();
            tblFav.Title = dtoFav.Title;
            tblFav.Fav = dtoFav.Fav;
            tblFav.Logo = dtoFav.Logo;
            tblFav.AddDate = DateTime.Now;
            tblFav.LastUpdeatedDate = DateTime.Now;
            tblFav.LastUpdatedUserID = session.UserID;
            int id = daoFav.AddFav(tblFav);
            LogBLL.AddLog(General.ProcessType.IconFavLogoAdded, General.TableName.Icon, id, session);
            return true;
        }

        public FavDTO GetFav()
        {
            return daoFav.GetFav();
        }

        public List<FavDTO> GetFavList()
        {
            List<FavDTO> list = new List<FavDTO>();
            list = daoFav.GetFavList();
            return list;
        }

        public FavDTO GetFavWithID(int id)
        {
           FavDTO dto = new FavDTO();
            dto = daoFav.GetFavWithID(id);
            return dto;
        }

        public FavDTO UpdateFav(FavDTO dtoFav,SessionDTO session)
        {
            FavDTO dTO = new FavDTO();
            dTO = daoFav.UpdateFav(dtoFav, session);
            LogBLL.AddLog(General.ProcessType.IconFavLogoUpdated, General.TableName.Icon, dTO.ID, session);
            return dTO;
        }

        public FavDTO UpdateFavWithID(FavDTO dtoFav,SessionDTO session)
        {
            FavDTO dTO = new FavDTO();
            dTO = daoFav.UpdateFavWithID(dtoFav, session);
            LogBLL.AddLog(General.ProcessType.IconFavLogoUpdated, General.TableName.Icon, dTO.ID, session);
            return dTO;
        }
    }
}
