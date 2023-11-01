using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class LogBLL
    {
        LogDAO daoLog = new LogDAO();

        public static void AddLog(int ProcessType, string TableName, int ProcessID,SessionDTO session)
        {
            LogDAO.AddLog(ProcessType, TableName, ProcessID,session);   
        }

        public List<LogDTO> GetLogs()
        {
            return daoLog.GetLogs();
        }
    }
}
