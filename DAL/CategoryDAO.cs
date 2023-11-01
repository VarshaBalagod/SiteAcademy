using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public static IEnumerable<SelectListItem> GetCategoryForDropdown()
        {
            try
            {
                IEnumerable<SelectListItem> categoryList = db.Categories.
                    Where(x => x.IsDeleted == false).
                    OrderByDescending(x => x.AddDate).
                    Select(x => new SelectListItem()
                    {
                        Text = x.CategoryName,
                        Value = SqlFunctions.StringConvert((double)x.ID)
                    }).ToList();
                return categoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCategory(Category tblCategory)
        {
            try
            {
                db.Categories.Add(tblCategory);
                db.SaveChanges();
                return tblCategory.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public List<Post> DeleteCategory(int id,SessionDTO session)
        {
            try
            {
                Category cat = db.Categories.First(x => x.ID == id);
                List<Post> postList = db.Posts.Where(x => x.CategoryID == id && x.IsDeleted == false).ToList();
                cat.IsDeleted = true;
                cat.DeletedDate = DateTime.Now;
                cat.LastUpdatedDate = DateTime.Now;
                cat.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
                return postList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }              

        public List<CategoryDTO> GetCategoryList()
        {
            try
            {
                List<CategoryDTO> dtoList= new List<CategoryDTO>();
                List<Category> tblList = db.Categories.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                foreach(var item in tblList)
                {
                    CategoryDTO category = new CategoryDTO();
                    category.ID = item.ID;
                    category.CategoryName = item.CategoryName;
                    dtoList.Add(category);
                }
                return dtoList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCategory(CategoryDTO categoryDTO,SessionDTO session)
        {
            try
            {
                Category tblCategory = db.Categories.First(x=>x.ID == categoryDTO.ID);
                tblCategory.ID = categoryDTO.ID;
                tblCategory.CategoryName = categoryDTO.CategoryName;
                tblCategory.LastUpdatedDate= DateTime.Now;
                tblCategory.LastUpdatedUserID = session.UserID;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
