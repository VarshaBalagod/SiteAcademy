using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address tblAddress)
        {
            try
            {
                db.Addresses.Add(tblAddress); 
                db.SaveChanges();
                return tblAddress.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAddress(int id, SessionDTO session)
        {
            try
            {
                Address tblAddress = db.Addresses.First(x => x.ID == id);
                tblAddress.IsDeleted = true;
                tblAddress.DeletedDate = DateTime.Now;
                tblAddress.LastUpdatedDate = DateTime.Now;
                tblAddress.LastUpdateUserID = session.UserID;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressDTO> GetAddressList()
        {
            try
            {
                List<Address> tblList = db.Addresses.Where(x => x.IsDeleted == false).ToList();
                List<AddressDTO>dtoList= new List<AddressDTO>();
                foreach(var item in tblList)
                {
                    AddressDTO dto = new AddressDTO();
                    dto.ID = item.ID;
                    dto.AddressContent = item.Address1;
                    dto.Email = item.Email;
                    dto.Phone= item.Phone;
                    dto.Phone2 = item.Phone2;
                    dto.Fax = item.Fax;
                    dto.MapPathLarge = item.MapPathLarge;
                    dto.MapPathSmall = item.MapPathSmall;
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressDTO GetAddressWithID(int id)
        {
            try
            {
                AddressDTO dTO = new AddressDTO();
                Address tblAddress = db.Addresses.First(x => x.ID == id);
                dTO.AddressContent = tblAddress.Address1;
                dTO.Email = tblAddress.Email;
                dTO.Phone = tblAddress.Phone;
                dTO.Phone2 = tblAddress.Phone2;
                dTO.Fax = tblAddress.Fax;
                dTO.MapPathLarge = tblAddress.MapPathLarge;
                dTO.MapPathSmall = tblAddress.MapPathSmall;
                return dTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAddress(AddressDTO addressDTO, SessionDTO session)
        {
            try
            {
                Address tblAddress = db.Addresses.First(x=>x.ID == addressDTO.ID);
                tblAddress.ID= addressDTO.ID;
                tblAddress.Address1 = addressDTO.AddressContent;
                tblAddress.Email = addressDTO.Email;
                tblAddress.Phone = addressDTO.Phone;
                tblAddress.Phone2 = addressDTO.Phone2;
                tblAddress.Fax = addressDTO.Fax;
                tblAddress.MapPathLarge= addressDTO.MapPathLarge;
                tblAddress.MapPathSmall= addressDTO.MapPathSmall;
                tblAddress.LastUpdatedDate = DateTime.Now;
                tblAddress.LastUpdateUserID = session.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
