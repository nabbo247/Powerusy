﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class powerusyDBCoreEntities : DbContext
    {
        public powerusyDBCoreEntities()
            : base("name=powerusyDBCoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_auditlog> tbl_auditlog { get; set; }
        public virtual DbSet<tbl_clearing> tbl_clearing { get; set; }
        public virtual DbSet<tbl_goodstype> tbl_goodstype { get; set; }
        public virtual DbSet<tbl_importation> tbl_importation { get; set; }
        public virtual DbSet<tbl_importation_document> tbl_importation_document { get; set; }
        public virtual DbSet<tbl_kyc> tbl_kyc { get; set; }
        public virtual DbSet<tbl_registered> tbl_registered { get; set; }
        public virtual DbSet<tbl_role> tbl_role { get; set; }
        public virtual DbSet<tbl_servicetype> tbl_servicetype { get; set; }
        public virtual DbSet<tbl_status> tbl_status { get; set; }
        public virtual DbSet<tbl_subgoodstype> tbl_subgoodstype { get; set; }
        public virtual DbSet<tbl_counntries> tbl_counntries { get; set; }
        public virtual DbSet<tbl_shipper> tbl_shipper { get; set; }
        public virtual DbSet<tbl_attachment> tbl_attachment { get; set; }
        public virtual DbSet<tbl_uploadDocType> tbl_uploadDocType { get; set; }
        public virtual DbSet<tbl_services> tbl_services { get; set; }
        public virtual DbSet<tbl_users> tbl_users { get; set; }
        public virtual DbSet<tbl_bidding> tbl_bidding { get; set; }
        public virtual DbSet<tbl_bidding_jobs> tbl_bidding_jobs { get; set; }
        public virtual DbSet<View_bid_jobs> View_bid_jobs { get; set; }
        public virtual DbSet<tbl_bidding_bookmark> tbl_bidding_bookmark { get; set; }
        public virtual DbSet<tbl_bidding_view> tbl_bidding_view { get; set; }
        public virtual DbSet<tbl_cars_models> tbl_cars_models { get; set; }
        public virtual DbSet<tbl_sea_ports> tbl_sea_ports { get; set; }
        public virtual DbSet<View_Shipper> View_Shipper { get; set; }
    }
}
