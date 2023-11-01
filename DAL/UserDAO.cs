using DTO;
using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public int AddUSer(T_User user)
        {
            try
            {
                db.T_User.Add(user); 
                db.SaveChanges();
                return user.ID;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public string DeleteUser(int id, SessionDTO session)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == id);
                user.IsDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdatedDate = DateTime.Now;
                user.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
                string imagePath = user.ImagePath;
                return imagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDTO> GetUserList()
        {
            List<UserDTO> dtoUser = db.T_User.Where(x => x.IsDeleted == false).OrderBy(x => x.AddDate)
                .Select(x => new UserDTO
                {
                    ID = x.ID,
                    Username = x.Username,
                    NameSurname = x.NameSurname,
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                    IsAdmin = x.IsAdmin
                }).ToList();

            return dtoUser;

            //List<UserDTO> dtoUser= new List<UserDTO>();
            // List<T_User> tblUsers = db.T_User.Where(x => x.IsDeleted == false).ToList();
            // foreach(var item in tblUsers)
            // {
            //     UserDTO dto = new UserDTO();
            //     dto.ID = item.ID;
            //     dto.Username = item.Username;
            //     dto.NameSurname = item.NameSurname;
            //     dto.Email = item.Email;
            //     dto.ImagePath = item.ImagePath;
            //     dto.IsAdmin = item.IsAdmin;
            //     dtoUser.Add(dto);
            // }
            // return dtoUser;
        }

        public UserDTO GetUserWithID(int id)
        {
            try
            {
                UserDTO dtoUser= new UserDTO();
                T_User tblUSer = db.T_User.First(x => x.ID == id);
                dtoUser.ID = tblUSer.ID;
                dtoUser.Username = tblUSer.Username;
                dtoUser.NameSurname=tblUSer.NameSurname;
                dtoUser.IsAdmin = tblUSer.IsAdmin;
                dtoUser.ImagePath = tblUSer.ImagePath;
                dtoUser.Email = tblUSer.Email;
                dtoUser.Password = tblUSer.Password;
                return dtoUser;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            try
            {
                UserDTO userdto = new UserDTO();
                T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                if (user != null && user.ID != 0)
                {
                    userdto.ID = user.ID;
                    userdto.Username = user.Username;
                    userdto.NameSurname = user.NameSurname;
                    userdto.Email = user.Email;
                    userdto.ImagePath = user.ImagePath;
                    userdto.IsAdmin = user.IsAdmin;
                }
                return userdto;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public string UpdateUser(UserDTO dtoUser, SessionDTO session)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == dtoUser.ID);
                user.Username = dtoUser.Username;
                user.Password = dtoUser.Password;
                user.IsAdmin = dtoUser.IsAdmin;
                user.Email = dtoUser.Email;
                string oldUserImage = user.ImagePath;
                if (dtoUser.ImagePath != null)
                    user.ImagePath = dtoUser.ImagePath;
                user.NameSurname = dtoUser.NameSurname;
                user.LastUpdatedUserID = session.UserID;
                user.LastUpdatedDate = DateTime.Now;
                db.SaveChanges();
                return oldUserImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
