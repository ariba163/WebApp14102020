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
    
    public partial class STPProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STPProductType()
        {
            this.STPProductBrands = new HashSet<STPProductBrand>();
        }
    
        public int ID { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STPProductBrand> STPProductBrands { get; set; }
    }
}