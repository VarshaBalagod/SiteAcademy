using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DTO;

namespace DAL
{
    public class LogDAO : PostContext
    {
        public static object Request { get; private set; }

        public static void AddLog(int ProcessType, string TableName, int ProcessID, SessionDTO session)
        {
            Log_Table log = new Log_Table();
            log.UserID = session.UserID;
            log.ProcessType = ProcessType;
            log.ProcessID = ProcessID;
            log.ProcessCategoryType = TableName;
            log.ProcessDate = DateTime.Now;
            log.IPAddress = HttpContext.Current.Request.UserHostAddress;
            db.Log_Table.Add(log);
            db.SaveChanges();
        }

        public List<LogDTO> GetLogs()
        {
            try
            {
                List<LogDTO> listDto= new List<LogDTO>();
                var list = (from l in db.Log_Table
                            join u in db.T_User on l.UserID equals u.ID
                            join p in db.ProcessTypes on l.ProcessID equals p.ID
                            select new
                            {
                                id = l.ID,
                                username = u.Username,
                                tablename = l.ProcessCategoryType,
                                tableid = l.ProcessID,
                                processname = p.ProcessName,
                                porcessdate = l.ProcessDate,
                                ipaddress = l.IPAddress
                            }).OrderByDescending(x => x.porcessdate).ToList();
               
                foreach (var item in list )
                {
                    LogDTO dtolog = new LogDTO();
                    dtolog.ID = item.id;
                    dtolog.UserName= item.username;
                    dtolog.TableID = item.tableid;
                    dtolog.TableName = item.tablename;
                    dtolog.ProcessDate = item.porcessdate;
                    dtolog.IPAddress = item.ipaddress;
                    dtolog.ProcessName = item.processname;
                    listDto.Add(dtolog);
                }
                return listDto;                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
