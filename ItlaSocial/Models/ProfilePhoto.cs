using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models
{
    public class ProfilePhoto
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        public string Description { get; set; }

        [Required]
        public PrivacyLevel PrivacyLevel { get; set; } = PrivacyLevel.Public;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public bool Current { get; set; } = true;

        [Required]
        public bool Reported { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
