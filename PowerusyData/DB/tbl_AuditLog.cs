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
    
    public partial class tbl_auditlog
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string operationperformed { get; set; }
        public string ipaddress { get; set; }
        public string pagevisited { get; set; }
        public Nullable<System.DateTimeOffset> dateaccessed { get; set; }
    }
}
