using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO daoCategory = new CategoryDAO();
        PostBLL bllPost = new PostBLL();

        public static IEnumerable<SelectListItem> GetCategoryForDropdown()
        {
            return CategoryDAO.GetCategoryForDropdown();
        }

        public bool AddCategory(CategoryDTO categoryDTO,SessionDTO session)
        {
            Category tblCategory = new Category();
            tblCategory.CategoryName = categoryDTO.CategoryName;
            tblCategory.AddDate=DateTime.Now;
            tblCategory.LastUpdatedDate = DateTime.Now;
            tblCategory.LastUpdatedUserID = session.UserID;
            int id = daoCategory.AddCategory(tblCategory);
            LogBLL.AddLog(General.ProcessType.CategoryAdded, General.TableName.Category, id, session);
            return true;
        }

        public List<PostImageDTO> DeleteCategory(int id, SessionDTO session)
        {

            List<Post> postList = daoCategory.DeleteCategory(id, session);
            LogBLL.AddLog(General.ProcessType.CategoryDeleted, General.TableName.Category, id, session);
            List<PostImageDTO> imageList = new List<PostImageDTO>();
            foreach(var item in postList)
            {
                List<PostImageDTO> postImages = bllPost.DeletePost(item.ID,session);
                LogBLL.AddLog(General.ProcessType.PostDeleted, General.TableName.Post, item.ID, session);
                foreach(var image in postImages)
                { 
                    imageList.Add(image);
                }
            }
            return imageList;            
        }

        public List<CategoryDTO> GetCategoryList()
        {
            return daoCategory.GetCategoryList();
        }

        public bool UpdateCategory(CategoryDTO categoryDTO, SessionDTO session)
        {
            daoCategory.UpdateCategory(categoryDTO, session);
            LogBLL.AddLog(General.ProcessType.CategoryUpdated, General.TableName.Category, categoryDTO.ID,session);
            return true;
        }
    }
}
