using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public static class General
    {
        public static class ProcessType
        {
            public static int Login = 1;
            public static int AddressAdded = 2;
            public static int AddressUpdated = 3;
            public static int AddressDeleted = 4;
            public static int AdsAdded = 5;
            public static int AdsUpdated = 6;
            public static int AdsDeleted = 7;
            public static int CategoryAdded = 8;
            public static int CategoryUpdated = 9;
            public static int CategoryDeleted = 10;
            public static int IconFavLogoAdded = 11;
            public static int IconFavLogoUpdated = 12;
            public static int IconFavLogoDeleted = 13;
            public static int MetaAdded = 14;
            public static int MetaUpdated = 15;
            public static int MetaDeleted = 16;
            public static int SocialMediaAdded = 17;
            public static int SocialMediaUpdated = 18;
            public static int SocialMediaDeleted = 19;
            public static int UserAdded = 20;
            public static int UserUpdated = 21;
            public static int UserDeleted = 22;
            public static int VideoAdded = 23;
            public static int VideoUpdated = 24;
            public static int VideoDeleted = 25;
            public static int PostAdded = 26;
            public static int PostUpdated = 27;
            public static int PostDeleted = 28;
            public static int ImageAdded = 29;
            public static int ImageUpdated = 30;
            public static int ImageDeleted = 31;
            public static int TagAdded = 32;
            public static int TagUpdated = 33;
            public static int TagDeleted = 34;
            public static int CommentApproved = 35;
            public static int CommentDeleted = 36;
            public static int ContactRead = 37;
            public static int ContactDeleted = 38;
        }

        public static class TableName
        {
            public static string Login = "Loging";
            public static string Address = "Address";
            public static string Ads = "Ad";
            public static string Category = "Category";
            public static string Icon = "IconFavTitle";
            public static string Meta = "Meta";
            public static string SocialMedia = "SocialMedia";
            public static string User = "User";
            public static string Video = "Video";
            public static string Post = "Post";
            public static string Image = "Image";
            public static string Tag = "Tag";
            public static string Comment = "Comment";
            public static string Contact = "Contact";
        }

        public static class Messages
        {
            public static int AddSuccess = 1;
            public static int EmptyArea = 2;
            public static int UpdateSuccess = 3;
            public static int ImageMissing = 4;
            public static int ExtensionError = 5;
            public static int GeneralError = 6;
            public static int NochangeError = 7;
        }
    }
}
