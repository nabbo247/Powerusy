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
    
    public partial class tbl_bidding
    {
        public int id { get; set; }
        public string GoodsType { get; set; }
        public string Name { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string service { get; set; }
        public string Consignee { get; set; }
        public string BookingNo { get; set; }
        public string BLNumber { get; set; }
        public string VesselName { get; set; }
        public string PortLoading { get; set; }
        public string PortDischarge { get; set; }
        public string ShippingLines { get; set; }
        public Nullable<decimal> EstBudget { get; set; }
        public string GoodDescription { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> statusid { get; set; }
        public string IconPath { get; set; }
    }
}
