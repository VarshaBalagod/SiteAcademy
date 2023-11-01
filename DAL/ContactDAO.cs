using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactDAO : PostContext
    {
        public void AddContact(Contact contact)
        {
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMessage(int iD, SessionDTO session)
        {
            try
            {
                Contact tblContact = db.Contacts.First(x=> x.ID == iD); 
                tblContact.IsDeleted = true;
                tblContact.DeletedDate = DateTime.Now;
                tblContact.LastUpdatedDate = DateTime.Now;
                tblContact.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ContactDTO> GetAllMessages()
        {
            try
            {
                List<ContactDTO> listDto = new List<ContactDTO>();
                List<Contact> listtbl = db.Contacts.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in listtbl)
                {
                    ContactDTO contact = new ContactDTO();
                    contact.ID = item.ID;
                    contact.Name = item.NameSurname;
                    contact.Email = item.Email;
                    contact.Subject = item.Subject;
                    contact.Message = item.Message;
                    contact.AddDate = item.AddDate;
                    contact.IsRead = item.IsRead;
                    listDto.Add(contact);
                }
                return listDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            try
            {
                List<ContactDTO>listDto= new List<ContactDTO>();
                List<Contact> listtbl = db.Contacts.Where(x => x.IsDeleted == false && x.IsRead == false).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in listtbl)
                {
                    ContactDTO contact = new ContactDTO();
                    contact.ID = item.ID;
                    contact.Name = item.NameSurname;
                    contact.Email=item.Email;
                    contact.Subject = item.Subject;
                    contact.Message = item.Message;
                    contact.AddDate=item.AddDate;
                    contact.IsRead = item.IsRead;
                    listDto.Add(contact);
                }
                return listDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReadMessage(int iD, SessionDTO session)
        {
            try
            {
                Contact contact = db.Contacts.First(x=>x.ID == iD); 
                contact.IsRead=true;
                contact.ReadUserID = session.UserID;
                contact.LastUpdatedDate = DateTime.Now;
                contact.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
