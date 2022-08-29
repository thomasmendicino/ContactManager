using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.DTOs
{
    public class VendorDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string Company { get; set; }

    }
}
