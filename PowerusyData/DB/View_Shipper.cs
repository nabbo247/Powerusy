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
    
    public partial class View_Shipper
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string username { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string phonenumber { get; set; }
        public Nullable<System.DateTimeOffset> dateadded { get; set; }
        public Nullable<int> roleid { get; set; }
        public Nullable<bool> status { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string authcode { get; set; }
        public Nullable<int> ShipID { get; set; }
        public Nullable<int> userid { get; set; }
        public string companyname { get; set; }
        public string tin { get; set; }
        public string tinpassword { get; set; }
        public string location { get; set; }
        public string ShipPhon { get; set; }
        public Nullable<int> statusid { get; set; }
        public Nullable<int> approvedby { get; set; }
        public string comment { get; set; }
        public string description { get; set; }
        public string workingdays { get; set; }
        public string workinghours { get; set; }
        public string bankname { get; set; }
        public string accountnumber { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
    }
}