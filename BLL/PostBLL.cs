using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostBLL
    {
        PostDAO daoPost = new PostDAO();

        public bool AddComment(GeneralDTO dtoGeneral)
        {
            Comment tblComment = new Comment();
            tblComment.PostID = dtoGeneral.PostID;
            tblComment.NameSurname = dtoGeneral.Name;
            tblComment.Email = dtoGeneral.Email;
            tblComment.CommentContent = dtoGeneral.Message;
            tblComment.AddDate = DateTime.Now;
            daoPost.AddComment(tblComment);
            return true;
        }

        public bool AddPost(PostDTO dtoPost,SessionDTO session)
        {
            Post tblPost = new Post();
            tblPost.Title = dtoPost.Title;
            tblPost.PostContent = dtoPost.PostContent;
            tblPost.ShortContent = dtoPost.ShortContent;
            tblPost.Slider = dtoPost.Slider;
            tblPost.Area1 = dtoPost.Area1;
            tblPost.Area2 = dtoPost.Area2;
            tblPost.Area3 = dtoPost.Area3;
            tblPost.Notification = dtoPost.Notification;
            tblPost.ViewCount = 0;
            tblPost.CategoryID = dtoPost.CategoryID;
            tblPost.SeoLink = SeoLink.GenerateUrl(dtoPost.Title);
            tblPost.LanguageName = dtoPost.LanguageName;
            tblPost.AddDate=DateTime.Now;
            tblPost.AddUserID = session.UserID;
            tblPost.LastUpdatedDate = DateTime.Now;
            tblPost.LastUpdatedUserID = session.UserID;
            int postID = daoPost.AddPost(tblPost);
            LogBLL.AddLog(General.ProcessType.PostAdded, General.TableName.Post, postID, session);
            SavePostImage(dtoPost.PostImages, postID, session);
            AddTag(dtoPost.TagText, postID, session);
            return true;
        }

        public void ApprovedComment(int iD,SessionDTO session)
        {
            daoPost.ApprovedComment(iD,session);
            LogBLL.AddLog(General.ProcessType.CommentApproved, General.TableName.Comment, iD, session);
        }

        public void DeleteComment(int iD, SessionDTO session)
        {
            daoPost.DeleteComment(iD, session);
            LogBLL.AddLog(General.ProcessType.CommentDeleted, General.TableName.Comment, iD, session);
        }

        public List<PostImageDTO> DeletePost(int id, SessionDTO session)
        {
            List<PostImageDTO> imageList = daoPost.DeletePost(id, session);
            LogBLL.AddLog(General.ProcessType.PostDeleted, General.TableName.Post, id, session);
            return imageList;
        }

        public string DeletePostImage(int id, SessionDTO session)
        {
            string imagePath = daoPost.DeletePostImage(id,session);
            LogBLL.AddLog(General.ProcessType.ImageDeleted, General.TableName.Image, id, session);
            return imagePath;
        }

        public List<CommentDTO> GetAllComments()
        {
            return daoPost.GetAllComments();
        }

        public CountDTO GetAllCount()
        {
            return daoPost.GetAllCounts();
        }

        public List<CommentDTO> GetComments()
        {
            return daoPost.GetComments();
        }

        public CountDTO GetCounts()
        {
            CountDTO dTO= new CountDTO();
            dTO.MessageCount = daoPost.GetMessageCount();
            dTO.CommentCount = daoPost.GetCommentCount();
            return dTO;
        }

        public List<PostDTO> GetPost()
        {
            return daoPost.GetPost();
        }

        public PostDTO GetPostWithID(int postID)
        {
           PostDTO dtoPost = new PostDTO();
            dtoPost = daoPost.GetPostWithID(postID);           
            dtoPost.PostImages = daoPost.GetPostImageWithId(postID);
            List<PostTag> taglist = daoPost.GetPostTagsWithID(postID);
            string tagvalue = "";
            foreach(var item in taglist)
            {
                tagvalue += item.TagContent;
                tagvalue += ",";
            }
            dtoPost.TagText = tagvalue;
            return dtoPost;
        }

        public bool UpdatePost(PostDTO dtoPost, SessionDTO session)
        {
            dtoPost.SeoLink = SeoLink.GenerateUrl(dtoPost.Title);
            daoPost.UpdatePost(dtoPost, session);
            LogBLL.AddLog(General.ProcessType.PostUpdated, General.TableName.Post, dtoPost.PostID,session);
            if (dtoPost.PostImages != null)
                SavePostImage(dtoPost.PostImages, dtoPost.PostID, session);
            daoPost.DeleteTags(dtoPost.PostID,session);
            AddTag(dtoPost.TagText, dtoPost.PostID, session);
            return true;
        }

        private void AddTag(string tagText, int postID, SessionDTO session)
        {
            string[] tags;
            tags=tagText.Split(',');
            List<PostTag> tagsList = new List<PostTag>();
            foreach (var item in tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = postID;
                tag.TagContent = item;
                tag.AddDate = DateTime.Now;
                tag.LastUpdatedDate = DateTime.Now;
                tag.LastUpdatedUserID = session.UserID;
                tagsList.Add(tag);
            }

            foreach(var tag in tagsList)
            {
                int tagId = daoPost.AddTag(tag);
                LogBLL.AddLog(General.ProcessType.TagAdded, General.TableName.Tag, tagId, session);
            }
        }

        void SavePostImage(List<PostImageDTO> list,int postID,SessionDTO session)
        {
            List<PostImage> imageList = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage img = new PostImage();
                img.PostID = postID;
                img.ImagePath = item.ImagePath;
                img.AddDate = DateTime.Now;
                img.LastUpdatedDate = DateTime.Now;
                img.LastUpdatedUserID = session.UserID;
                imageList.Add(img);
            }
            foreach (var item in imageList)
            {
                int imageID = daoPost.AddImage(item);
                LogBLL.AddLog(General.ProcessType.ImageAdded, General.TableName.Image, imageID,session);
            }
        }
    }
}
