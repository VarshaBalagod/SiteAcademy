using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO daoMeta = new MetaDAO();

        public bool AddMeta(MetaDTO model, SessionDTO session)
        {
            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate=DateTime.Now;
            meta.LastUpdatedUserID=session.UserID;
            meta.LastUpdatedDate=DateTime.Now;
            int metaID = daoMeta.AddMeta(meta);
            LogDAO.AddLog(General.ProcessType.MetaAdded, General.TableName.Meta, metaID, session);
            return true;
        }

        public void DeleteMeta(int iD,SessionDTO session)
        {
           daoMeta.DeleteMeta(iD, session);
            LogBLL.AddLog(General.ProcessType.MetaDeleted, General.TableName.Meta, iD, session);
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> dtoMeta = new List<MetaDTO>();
            dtoMeta = daoMeta.GetMetaDat();
            return dtoMeta;
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            MetaDTO dtoMeta = new MetaDTO();
            dtoMeta = daoMeta.GetMetaWithID(ID);
            return dtoMeta;
        }

        public bool UpdateMeta(MetaDTO dtoMeta,SessionDTO session)
        {
            daoMeta.UpdateMeta(dtoMeta, session);
            LogDAO.AddLog(General.ProcessType.MetaUpdated, General.TableName.Meta, dtoMeta.MetaID, session);
            return true;
        }
    }
}
