//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppSastiServices.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRNInvoice
    {
        public int ID { get; set; }
        public int STPOrdersID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> QTY { get; set; }
        public decimal Amount { get; set; }
    
        public virtual STPOrder STPOrder { get; set; }
    }
}