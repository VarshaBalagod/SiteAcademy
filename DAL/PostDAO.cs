using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAO
    {

        //   using (POSTDATAEntities db = new POSTDATAEntities())
       //            {  }
            //for speed up data
            //    select commant also for speed up

        public void AddComment(Comment tblComment)
        {
            try
            {
                using(POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Comments.Add(tblComment);
                    db.SaveChanges();
                }              
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }
                return item.ID;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public int AddPost(Post tblPost)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Posts.Add(tblPost);
                    db.SaveChanges();
                }
                return tblPost.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddTag(PostTag tag)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostTags.Add(tag);
                    db.SaveChanges();
                }
                return tag.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApprovedComment(int iD, SessionDTO session)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Comment tblComment = db.Comments.First(x => x.ID == iD);
                    tblComment.IsApproved = true;
                    tblComment.ApprovedDate = DateTime.Now;
                    tblComment.ApprovedUserID = session.UserID;
                    tblComment.LastUpdatedDate = DateTime.Now;
                    tblComment.LastUpdatedUserID = session.UserID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteComment(int iD, SessionDTO session)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Comment tblComment = db.Comments.First(x => x.ID == iD);
                    tblComment.IsDeleted = true;
                    tblComment.DeletedDate = DateTime.Now;
                    tblComment.LastUpdatedDate = DateTime.Now;
                    tblComment.LastUpdatedUserID = session.UserID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int id, SessionDTO session)
        {
            try
            {
                List<PostImageDTO> imageDto = new List<PostImageDTO>();

                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Post post = db.Posts.First(x => x.ID == id);
                    post.IsDeleted = true;
                    post.DeletedDate = DateTime.Now;
                    post.LastUpdatedDate = DateTime.Now;
                    post.LastUpdatedUserID = session.UserID;
                    db.SaveChanges();

                    List<PostImage> imageList = db.PostImages.Where(x => x.PostID == id).ToList();
                    foreach (var image in imageList)
                    {
                        PostImageDTO dTO = new PostImageDTO();
                        dTO.PostID = image.PostID;
                        dTO.PostImageID = image.ID;
                        dTO.ImagePath = image.ImagePath;
                        image.IsDeleted = true;
                        image.DeletedDate = DateTime.Now;
                        image.LastUpdatedDate = DateTime.Now;
                        image.LastUpdatedUserID = session.UserID;
                        imageDto.Add(dTO);
                    }
                    db.SaveChanges();
                }
                return imageDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeletePostImage(int id, SessionDTO session)
        {
            try
            {
                string imagePath = "";
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    PostImage image = db.PostImages.First(x => x.ID == id);
                    imagePath = image.ImagePath;
                    image.IsDeleted = true;
                    image.DeletedDate = DateTime.Now;
                    image.LastUpdatedDate = DateTime.Now;
                    image.LastUpdatedUserID = session.UserID;
                    db.SaveChanges();
                }
                return imagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTags(int postID, SessionDTO session)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    List<PostTag> list = db.PostTags.Where(x => x.IsDeleted == false && x.PostID == postID).ToList();
                    foreach (var tag in list)
                    {
                        tag.IsDeleted = false;
                        tag.DeletedDate = DateTime.Now;
                        tag.LastUpdatedDate = DateTime.Now;
                        tag.LastUpdatedUserID = session.UserID;
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CommentDTO> GetAllComments()
        {
            try
            {
                List<CommentDTO> listComment = new List<CommentDTO>();

                using (POSTDATAEntities db = new POSTDATAEntities())
                {                  
                    var list = (from c in db.Comments.Where(x => x.IsDeleted == false)
                                join p in db.Posts on c.PostID equals p.ID
                                select new
                                {
                                    CommentID = c.ID,
                                    PostTitle = p.Title,
                                    Email = c.Email,
                                    content = c.CommentContent,
                                    adddate = c.AddDate,
                                    isapproved = c.IsApproved
                                }).OrderByDescending(x => x.adddate).ToList();

                    foreach (var item in list)
                    {
                        CommentDTO dto = new CommentDTO();
                        dto.ID = item.CommentID;
                        dto.AddDate = item.adddate;
                        dto.CommentContent = item.content;
                        dto.Email = item.Email;
                        dto.PostTitle = item.PostTitle;
                        dto.IsApproved = item.isapproved;
                        listComment.Add(dto);
                    }

                }
                return listComment;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int GetCommentCount()
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    return db.Comments.Where(x => x.IsDeleted == false && x.IsApproved == false).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetMessageCount()
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    return db.Contacts.Where(x => x.IsDeleted == false && x.IsRead == false).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommentDTO> GetComments()
        {
            try
            {
                List<CommentDTO> commentsList = new List<CommentDTO>();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    var list = (from c in db.Comments.Where(x => x.IsDeleted == false && x.IsApproved == false)
                                join p in db.Posts on c.PostID equals p.ID
                                select new
                                {
                                    CommentID = c.ID,
                                    PostTitle = p.Title,
                                    Email = c.Email,
                                    content = c.CommentContent,
                                    adddate = c.AddDate
                                }).OrderByDescending(x => x.adddate).ToList();

                    foreach (var item in list)
                    {
                        CommentDTO dto = new CommentDTO();
                        dto.ID = item.CommentID;
                        dto.AddDate = item.adddate;
                        dto.CommentContent = item.content;
                        dto.Email = item.Email;
                        dto.PostTitle = item.PostTitle;
                        commentsList.Add(dto);
                    }

                }
                return commentsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetHotnews()
        {
            try
            {
                List<PostDTO> postList = new List<PostDTO>();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    var postTbl = (from p in db.Posts
                                   where p.IsDeleted == false && p.Area1 == true
                                   join c in db.Categories on p.CategoryID equals c.ID
                                   select new
                                   {
                                       ID = p.ID,
                                       Title = p.Title,
                                       categoryname = c.CategoryName,
                                       adddate = p.AddDate,
                                       seolink = p.SeoLink
                                   }).OrderByDescending(x => x.adddate).Take(8).ToList();


                    foreach (var post in postTbl)
                    {
                        PostDTO dTO = new PostDTO();
                        dTO.PostID = post.ID;
                        dTO.Title = post.Title;
                        dTO.CategoryName = post.categoryname;
                        dTO.SeoLink = post.seolink;
                        dTO.AddDate = post.adddate;
                        postList.Add(dTO);
                    }
                }
                return postList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public List<PostDTO> GetPost()
        {
            try
            {

                List<PostDTO> postList = new List<PostDTO>();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    postList = (from p in db.Posts
                                where p.IsDeleted == false
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryname = c.CategoryName,
                                    adddate = p.AddDate
                                }).OrderByDescending(x => x.adddate)
                                   .Select(x => new PostDTO
                                   {
                                       PostID = x.ID,
                                       Title = x.Title,
                                       CategoryName = x.categoryname,
                                       AddDate = x.adddate
                                   }).ToList();
                }
                return postList;



                //List<PostDTO> postList= new List<PostDTO>();
                //using (POSTDATAEntities db = new POSTDATAEntities())
                //{
                //    var postTbl = (from p in db.Posts
                //                   where p.IsDeleted == false
                //                   join c in db.Categories on p.CategoryID equals c.ID
                //                   select new
                //                   {
                //                       ID = p.ID,
                //                       Title = p.Title,
                //                       categoryname = c.CategoryName,
                //                       adddate = p.AddDate
                //                   }).OrderByDescending(x => x.adddate).ToList();


                //    foreach (var post in postTbl)
                //    {
                //        PostDTO dTO = new PostDTO();
                //        dTO.PostID = post.ID;
                //        dTO.Title = post.Title;
                //        dTO.CategoryName = post.categoryname;
                //        dTO.AddDate = post.adddate;
                //        postList.Add(dTO);
                //    }
                //}
                //return postList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostImageDTO> GetPostImageWithId(int postID)
        {
            try
            {
                List<PostImageDTO> dtoList = new List<PostImageDTO>();

                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    List<PostImage> list = db.PostImages.Where(x => x.IsDeleted == false && x.PostID == postID).ToList();                  
                    foreach (var image in list)
                    {
                        PostImageDTO dTO = new PostImageDTO();
                        dTO.PostImageID = image.ID;
                        dTO.ImagePath = image.ImagePath;
                        dtoList.Add(dTO);
                    }
                }
                return dtoList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PostTag> GetPostTagsWithID(int postID)
        {
            List<PostTag> dtoList = new List<PostTag>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                dtoList = db.PostTags.Where(x => x.IsDeleted == false && x.PostID == postID).ToList();
            }
            return dtoList;
        }

        public PostDTO GetPostWithID(int postID)
        {
            try
            {
                PostDTO dTO = new PostDTO();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Post tblPost = db.Posts.First(x => x.ID == postID);                   
                    dTO.PostID = tblPost.ID;
                    dTO.Title = tblPost.Title;
                    dTO.ShortContent = tblPost.ShortContent;
                    dTO.PostContent = tblPost.PostContent;
                    dTO.LanguageName = tblPost.LanguageName;
                    dTO.SeoLink = tblPost.SeoLink;
                    dTO.Slider = tblPost.Slider;
                    dTO.Area1 = tblPost.Area1;
                    dTO.Area2 = tblPost.Area2;
                    dTO.Area3 = tblPost.Area3;
                    dTO.Notification = tblPost.Notification;
                    dTO.CategoryID = tblPost.CategoryID;
                }
                return dTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePost(PostDTO dtoPost, SessionDTO session)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Post post = db.Posts.First(x => x.ID == dtoPost.PostID);
                    post.Title = dtoPost.Title;
                    post.ShortContent = dtoPost.ShortContent;
                    post.PostContent = dtoPost.PostContent;
                    post.LanguageName = dtoPost.LanguageName;
                    post.SeoLink = dtoPost.SeoLink;
                    post.Slider = dtoPost.Slider;
                    post.Area1 = dtoPost.Area1;
                    post.Area2 = dtoPost.Area2;
                    post.Area3 = dtoPost.Area3;
                    post.Notification = dtoPost.Notification;
                    post.CategoryID = dtoPost.CategoryID;
                    post.LastUpdatedDate = DateTime.Now;
                    post.LastUpdatedUserID = session.UserID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CountDTO GetAllCounts()
        {
            try
            {
                CountDTO dtoCount = new CountDTO();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    dtoCount.PostCount = db.Posts.Where(x => x.IsDeleted == false).Count();
                    dtoCount.CommentCount = db.Comments.Where(x => x.IsDeleted == false).Count();
                    dtoCount.MessageCount = db.Contacts.Where(x => x.IsDeleted == false).Count();
                    dtoCount.ViewCount = db.Posts.Where(x => x.IsDeleted == false).Sum(x => x.ViewCount);
                }
                return dtoCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
