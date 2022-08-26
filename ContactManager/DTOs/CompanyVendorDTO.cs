using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.DTOs
{
    public class CompanyVendorDTO
    {
        // This doesn't seem to be a sequential guid which could lead to performance problems.
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string VendorCode { get; set; }
    }
}
