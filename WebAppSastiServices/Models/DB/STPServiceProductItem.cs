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
    
    public partial class STPServiceProductItem
    {
        public int ID { get; set; }
        public string ServiceProductName { get; set; }
        public string ServiceModelNo { get; set; }
        public string ServiceProductDescription { get; set; }
        public int STPProductBrandID { get; set; }
        public bool IsAvailible { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ImageFilePath { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int FuelTypeId { get; set; }
        public int UnitTypeId { get; set; }
    
        public virtual STPProductBrand STPProductBrand { get; set; }
        public virtual STPServicesFuelType STPServicesFuelType { get; set; }
        public virtual STPServicesUnitType STPServicesUnitType { get; set; }
    }
}