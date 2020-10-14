using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppSastiServices.Models.DB
{

    [MetadataType(typeof(TRNCustomerOrderMetadata))]
    public partial class TRNCustomerOrder
    {
    }

    public class TRNCustomerOrderMetadata
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Contact #")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string OrderDescription { get; set; }

        [Required]
        [Display(Name = "preferred Date")]
        public DateTime preferredDate { get; set; }

        
        [Display(Name = "preferred Time")]
        public int preferredTimeID { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceTypeId { get; set; }
        [Required]
        [Display(Name = "Fuel Type")]
        public int FuelTypeId { get; set; }

        [Required]
        [Display(Name = "Unit Type")]
        public int UnitTypeId { get; set; }

        [Display(Name = "Order Status")]
        public int OrderStatusId { get; set; }
        public DateTime? CreateOn { get; set; }

    }
}