//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PowerusyData.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_importation_document
    {
        public int id { get; set; }
        public string importationid { get; set; }
        public string documentname { get; set; }
        public string documentpath { get; set; }
        public Nullable<System.DateTimeOffset> dateadded { get; set; }
        public Nullable<int> statusid { get; set; }
        public string approvedby { get; set; }
        public string dateapproved { get; set; }
        public string approvalcomment { get; set; }
    }
}
