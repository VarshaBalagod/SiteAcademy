using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO daoAddress = new AddressDAO();

        public bool AddAddress(AddressDTO dtoAddress, SessionDTO session)
        {
            Address tblAddress = new Address();
            tblAddress.Address1 = dtoAddress.AddressContent;
            tblAddress.Email = dtoAddress.Email;
            tblAddress.Phone = dtoAddress.Phone;
            tblAddress.Phone2 = dtoAddress.Phone2;
            tblAddress.Fax = dtoAddress.Fax;
            tblAddress.MapPathLarge = dtoAddress.MapPathLarge;
            tblAddress.MapPathSmall = dtoAddress.MapPathSmall;
            tblAddress.AddDate = DateTime.Now;
            tblAddress.LastUpdatedDate = DateTime.Now;
            tblAddress.LastUpdateUserID = session.UserID;
            int id = daoAddress.AddAddress(tblAddress);
            LogBLL.AddLog(General.ProcessType.AddressAdded, General.TableName.Address, id, session);
            return true;
        }

        public void DeleteAddress(int id, SessionDTO session)
        {
            daoAddress.DeleteAddress(id, session);
            LogBLL.AddLog(General.ProcessType.AddressDeleted, General.TableName.Address, id,session);
        }

        public List<AddressDTO> GetAddressList()
        {
            return daoAddress.GetAddressList();
        }

        public AddressDTO GetAddressWithID(int id)
        {
            return daoAddress.GetAddressWithID(id);
        }

        public bool UpdateAddress(AddressDTO addressDTO, SessionDTO session)
        {
            daoAddress.UpdateAddress(addressDTO, session);
            LogBLL.AddLog(General.ProcessType.AddressUpdated, General.TableName.Address, addressDTO.ID, session);
            return true;

        }
    }
}
