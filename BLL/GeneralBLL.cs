using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO daoGeneral = new GeneralDAO();
        AdsDAO daoAds = new AdsDAO();
        PostDAO daoPost = new PostDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        AddressDAO daoAddress = new AddressDAO();

        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dtoGeneral= new GeneralDTO();
            dtoGeneral.SliderPost = daoGeneral.GetSliderPost();
            dtoGeneral.BreakingPost = daoGeneral.GetBrakingPost();
            dtoGeneral.PopularPost = daoGeneral.GetPopularPost();
            dtoGeneral.MostViewedPost = daoGeneral.GetMostViewedPost();
            dtoGeneral.Videos = daoGeneral.GetVideos();
            dtoGeneral.AdsList = daoAds.GetAdsList();
           
            return dtoGeneral;
        }

        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral.BreakingPost = daoGeneral.GetBrakingPost();
            dtoGeneral.AdsList = daoAds.GetAdsList();
            if (categoryName == "Video")
            {
                dtoGeneral.Videos = daoGeneral.GetAllVideos();
            }
            else
            {
                List<CategoryDTO> listCategory = daoCategory.GetCategoryList();
                int categoryID = 0;
                foreach(var item in listCategory)
                {
                    if (categoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dtoGeneral.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dtoGeneral.CategoryPostList = daoGeneral.GetCategoryPostList(categoryID);

            }
            return dtoGeneral;
        }

        public GeneralDTO GetContactPageItem()
        {
            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral.BreakingPost= daoGeneral.GetBrakingPost();
            dtoGeneral.AdsList= daoAds.GetAdsList();
            dtoGeneral.Address = daoAddress.GetAddressList().First();
            return dtoGeneral;
        }

        public GeneralDTO GetPostDetailPageItemWithID(int iD)
        {
            GeneralDTO dtoGeneral = new GeneralDTO();
            dtoGeneral.BreakingPost =daoGeneral.GetBrakingPost();   
            dtoGeneral.AdsList=daoAds.GetAdsList();
            dtoGeneral.PostDetail = daoGeneral.GetPostDetail(iD);
            return dtoGeneral;
        }

        public GeneralDTO GetSearchPost(string searchText)
        {
            GeneralDTO dtoGen= new GeneralDTO();
            dtoGen.BreakingPost=daoGeneral.GetBrakingPost();
            dtoGen.AdsList= daoAds.GetAdsList();
            dtoGen.CategoryPostList = daoGeneral.GetSearchPost(searchText);
            dtoGen.SearchText = searchText;
            return dtoGen;
        }
    }
}
