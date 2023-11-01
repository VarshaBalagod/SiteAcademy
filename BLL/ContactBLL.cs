using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContactBLL
    {
        ContactDAO daoContact = new ContactDAO();

        public bool AddContact(GeneralDTO dtoGen)
        {
           Contact contact = new Contact();
            contact.NameSurname = dtoGen.Name;
            contact.Subject = dtoGen.Subject;
            contact.Email = dtoGen.Email;
            contact.Message = dtoGen.Message;
            contact.AddDate=DateTime.Now;
            contact.LastUpdatedDate=DateTime.Now;
            daoContact.AddContact(contact);
            return true;
        }

        public void DeleteMessage(int iD,SessionDTO session)
        {
            daoContact.DeleteMessage(iD, session);
            LogBLL.AddLog(General.ProcessType.ContactDeleted, General.TableName.Contact, iD, session);
        }

        public List<ContactDTO> GetAllMessages()
        {
            return daoContact.GetAllMessages();
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            return daoContact.GetUnreadMessages();
        }

        public void ReadMessage(int iD, SessionDTO session)
        {
            daoContact.ReadMessage(iD, session);
            LogBLL.AddLog(General.ProcessType.ContactRead, General.TableName.Contact, iD, session);
        }
    }
}
