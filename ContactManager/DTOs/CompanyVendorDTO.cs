using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.DTOs
{
    public class CompanyVendorDTO
    {
        // Make this a sequential guid to avoid performance problems.
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string VendorCode { get; set; }
    }
}
