using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GeneralDTO
    {
        public List<PostDTO> SliderPost { get; set; }
        public List<PostDTO> PopularPost { get; set; }
        public List<PostDTO> MostViewedPost { get; set; }
        public List<PostDTO> BreakingPost { get; set; }

        public List<VideoDTO> Videos { get; set; }

        public List<AdsDTO> AdsList { get; set; }
        
        //post detail page
        public PostDTO PostDetail { get; set; }

        //comment form
        public int PostID { get; set; }

        // comment form and contact form
        public string Email { get;set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        

        //Category detail page
        public List<PostDTO> CategoryPostList { get; set; }
        public string CategoryName { get; set; }

        //contact page
        public AddressDTO Address { get; set; }

        //search
        public string SearchText { get; set; }
    }
}
