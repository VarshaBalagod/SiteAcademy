using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<VideoDTO> GetAllVideos()
        {
            try
            {
                List<VideoDTO> dtoVideos = new List<VideoDTO>();
                List<Video> tblVideos = db.Videos.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in tblVideos)
                {
                    VideoDTO dto = new VideoDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dto.AddDate = item.AddDate;
                    dtoVideos.Add(dto);
                }
                return dtoVideos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetBrakingPost()
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false && x.Slider == false)
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.adddate).Take(5).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate = item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false && x.CategoryID == categoryID)
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.adddate).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate = item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetMostViewedPost()
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false)
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.videocount).Take(5).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate = item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetPopularPost()
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false && x.Area2 == true)
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.adddate).Take(5).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate = item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PostDTO GetPostDetail(int iD)
        {
            try
            {
                Post tbPost = db.Posts.First(x => x.ID == iD);
                tbPost.ViewCount++;
                db.SaveChanges();

                PostDTO dto = new PostDTO();
                dto.PostID = tbPost.ID;
                dto.Title = tbPost.Title;
                dto.ShortContent = tbPost.ShortContent;
                dto.PostContent = tbPost.PostContent;
                dto.SeoLink = tbPost.SeoLink;
                dto.LanguageName = tbPost.LanguageName;
                dto.CategoryID = tbPost.CategoryID;
                dto.CategoryName = (db.Categories.First(x => x.ID == dto.CategoryID)).CategoryName;
               
                List<PostImage> listImage = db.PostImages.Where(x => x.PostID == iD && x.IsDeleted == false).ToList();
                List<PostImageDTO> listdtoImg = new List<PostImageDTO>();
                foreach (var image in listImage)
                {
                    PostImageDTO dtoimg = new PostImageDTO();
                    dtoimg.PostID = image.PostID;
                    dtoimg.PostImageID = image.ID;
                    dtoimg.ImagePath = image.ImagePath;
                    listdtoImg.Add(dtoimg);
                }
                dto.PostImages = listdtoImg;
               
                dto.CommentCount = db.Comments.Where(x => x.PostID == iD && x.IsDeleted == false && x.IsApproved == true).Count();

                List<Comment> tblComment = db.Comments.Where(x => x.PostID == iD && x.IsDeleted == false && x.IsApproved == true).ToList();
                List<CommentDTO> dtoComList = new List<CommentDTO>();
                foreach(var item in tblComment)
                {
                    CommentDTO dtocom = new CommentDTO();
                    dtocom.ID = item.ID;
                    dtocom.PostID= item.PostID;
                    dtocom.AddDate = item.AddDate;
                    dtocom.CommentContent = item.CommentContent;
                    dtocom.Name = item.NameSurname;
                    dtocom.Email = item.Email;
                    dtoComList.Add(dtocom);
                }
                dto.CommentList = dtoComList;

                List<PostTag> tbltag = db.PostTags.Where(x => x.PostID == iD && x.IsDeleted == false).ToList();
                List<PostTagDTO> dtolisttag = new List<PostTagDTO>();
                foreach(var item in tbltag)
                {
                    PostTagDTO dtotag = new PostTagDTO();
                    dtotag.PostTagID = item.ID;
                    dtotag.TagContent = item.TagContent;
                    dtolisttag.Add(dtotag);
                }
                dto.TagList = dtolisttag;

                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetSearchPost(string searchText)
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false && (x.Title.Contains(searchText)|| x.PostContent.Contains(searchText)))
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.adddate).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate = item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetSliderPost()
        {
            try
            {
                List<PostDTO> dtoList = new List<PostDTO>();
                var list = (from p in db.Posts.Where(x => x.IsDeleted == false && x.Slider == true)
                            join c in db.Categories on p.CategoryID equals c.ID
                            select new
                            {
                                PostID = p.ID,
                                CategoryID = p.CategoryID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                seolink = p.SeoLink,
                                videocount = p.ViewCount,
                                adddate = p.AddDate
                            }).OrderByDescending(x => x.adddate).Take(8).ToList();


                foreach (var item in list)
                {
                    PostDTO dto = new PostDTO();
                    dto.PostID = item.PostID;
                    dto.CategoryID = item.CategoryID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.categoryname;
                    dto.SeoLink = item.seolink;
                    dto.AddDate= item.adddate;
                    dto.ViewCount = item.videocount;
                    PostImage images = db.PostImages.First(x => x.IsDeleted == false && x.PostID == item.PostID);
                    dto.ImagePath = images.ImagePath;
                    dto.CommentCount = db.Comments.Where(x => x.IsDeleted == false && x.PostID == item.PostID && x.IsApproved == true).Count();
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<VideoDTO> GetVideos()
        {
            try
            {
                List<VideoDTO> dtoVideos= new List<VideoDTO>();
                List<Video> tblVideos = db.Videos.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Take(3).ToList();
                foreach(var item in tblVideos)
                {
                    VideoDTO dto= new VideoDTO();
                    dto.ID= item.ID;
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dto.AddDate = item.AddDate;
                    dtoVideos.Add(dto);
                }
                return dtoVideos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
