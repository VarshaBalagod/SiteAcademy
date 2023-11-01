using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DAL
{
    public class MetaDAO : PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();
                return meta.ID;
            }
            catch(Exception ex) 
            {
                throw ex;
           }
        }

        public void DeleteMeta(int iD,SessionDTO session)
        {
            try
            {
                Meta tbl = db.Metas.First(m => m.ID == iD);
                tbl.IsDeleted = true;
                tbl.DeletedDate = DateTime.Now;
                tbl.LastUpdatedDate = DateTime.Now;
                tbl.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDTO> GetMetaDat()
        {
            try 
            {
                List<MetaDTO> dtoMeta =new List<MetaDTO>();
                List<Meta> list = db.Metas.Where(x => x.IsDeleted == false).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    MetaDTO dto = new MetaDTO();
                    dto.MetaID = item.ID;
                    dto.Name = item.Name;
                    dto.MetaContent = item.MetaContent;
                    dtoMeta.Add(dto);
                }

                return dtoMeta;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            try
            {
                Meta tableMeta = db.Metas.First(x => x.ID == ID);
                MetaDTO dto = new MetaDTO();
                dto.MetaID = tableMeta.ID;
                dto.Name = tableMeta.Name;
                dto.MetaContent= tableMeta.MetaContent;                
                return dto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMeta(MetaDTO dtoMeta,SessionDTO session)
        {
            try
            {
                Meta tableMeta = db.Metas.First(x => x.ID == dtoMeta.MetaID);
                tableMeta.Name = dtoMeta.Name;
                tableMeta.MetaContent = dtoMeta.MetaContent;
                tableMeta.LastUpdatedDate = DateTime.Now;
                tableMeta.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
