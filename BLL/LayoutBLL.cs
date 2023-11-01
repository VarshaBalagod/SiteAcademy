using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO daoCategory = new CategoryDAO();
        SocialMediaDAO daoSM = new SocialMediaDAO();
        FavDAO daoFav = new FavDAO();
        MetaDAO daoMeta = new MetaDAO();
        AddressDAO daoAddress = new AddressDAO();
        PostDAO daoPost = new PostDAO();

        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dtoLayout = new HomeLayoutDTO();
            dtoLayout.Categories = daoCategory.GetCategoryList();
            List<SocialMediaDTO> listSM = new List<SocialMediaDTO>();
            listSM = daoSM.GetSocialMedia();
            dtoLayout.Facebook = listSM.First(x => x.Link.Contains("facebook"));
            dtoLayout.Twitter = listSM.First(x => x.Link.Contains("twitter"));
            dtoLayout.LinkedIn = listSM.First(x => x.Link.Contains("linkedin"));
            dtoLayout.Youtube = listSM.First(x => x.Link.Contains("youtube"));
            dtoLayout.GooglePlus = listSM.First(x => x.Link.Contains("google"));
            dtoLayout.Instagram = listSM.First(x => x.Link.Contains("instagram"));
            dtoLayout.FavDTO = daoFav.GetFav();
            dtoLayout.MetaList = daoMeta.GetMetaDat();
            List<AddressDTO>  listAddress = daoAddress.GetAddressList();
            dtoLayout.Address = listAddress.First();
            dtoLayout.Hotnews = daoPost.GetHotnews();

            
            return dtoLayout;
        }
    }
}
