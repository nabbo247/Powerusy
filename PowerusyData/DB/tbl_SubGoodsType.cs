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
    
    public partial class tbl_SubGoodsType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SubGoodsType()
        {
            this.tbl_Importation = new HashSet<tbl_Importation>();
        }
    
        public int Id { get; set; }
        public Nullable<int> GoodsTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> tbl_GoodsTypeId { get; set; }
    
        public virtual tbl_GoodsType tbl_GoodsType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Importation> tbl_Importation { get; set; }
    }
}
