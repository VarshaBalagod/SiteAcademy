//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Video
    {
        public int ID { get; set; }
        public string VideoPath { get; set; }
        public string Title { get; set; }
        public string OriginalVideoPath { get; set; }
        public int AddUserID { get; set; }
        public System.DateTime AddDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int LastUpdatedUserID { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    
        public virtual T_User T_User { get; set; }
    }
}
