using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PostTagDTO
    {
        public int PostTagID { get; set; }
        public int PostID { get; set; }
        public string TagContent { get; set; }
    }
}
