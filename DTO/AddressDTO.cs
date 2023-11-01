using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddressDTO
    {
        public int ID {  get; set; }
        [Required (ErrorMessage ="Please fill the address area.")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Please fill the email area.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill the phone area.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please fill the phone 2 area.")]
        public string Phone2 { get; set; }
        [Required(ErrorMessage = "Please fill the fax area.")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "Please fill the map area.")]
        public string MapPathLarge { get; set; }
        [Required(ErrorMessage = "Please fill the map area.")]
        public string MapPathSmall { get; set; }
    }
}
