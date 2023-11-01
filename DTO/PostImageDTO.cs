using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PostImageDTO
    {
        public int PostImageID { get; set; }
        public int PostID { get; set; }
        public string ImagePath { get; set;}
    }
}
