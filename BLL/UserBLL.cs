using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        UserDAO daoUser = new UserDAO();

        public bool AddUser(UserDTO dtoUser, SessionDTO session)
        {
            T_User user = new T_User();
            user.Username= dtoUser.Username;
            user.Password= dtoUser.Password;
            user.NameSurname= dtoUser.NameSurname;
            user.Email = dtoUser.Email;
            user.ImagePath = dtoUser.ImagePath;
            user.IsAdmin = dtoUser.IsAdmin;
            user.AddDate = DateTime.Now;
            user.LastUpdatedDate = DateTime.Now;
            user.LastUpdatedUserID = session.UserID;
            user.IsDeleted = false;
            int ID = daoUser.AddUSer(user);
            LogDAO.AddLog(General.ProcessType.UserAdded, General.TableName.User, ID, session);
            return true;
        }

        public string DeleteUser(int id,SessionDTO session)
        {
            string imagePath = daoUser.DeleteUser(id,session);
            LogDAO.AddLog(General.ProcessType.UserDeleted, General.TableName.User, id, session);
            return imagePath;
        }

        public List<UserDTO> GetUserList()
        {
           List<UserDTO> dtoUser= new List<UserDTO>();
            dtoUser = daoUser.GetUserList();
            return dtoUser;
        }

        public UserDTO GetUserWithID(int id)
        {
            return daoUser.GetUserWithID(id);
        }

        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO userdto = new UserDTO();
            userdto = daoUser.GetUserWithUsernameAndPassword(model);
            return userdto;
        }

        public string UpdateUser(UserDTO dtoUser, SessionDTO session)
        {
            string oldUserImage = daoUser.UpdateUser(dtoUser, session);
            LogDAO.AddLog(General.ProcessType.UserUpdated, General.TableName.User, dtoUser.ID, session);
            return oldUserImage;
        }
    }
}
