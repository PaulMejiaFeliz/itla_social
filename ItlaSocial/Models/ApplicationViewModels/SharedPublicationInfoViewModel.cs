using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class SharedPublicationInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public PrivacyLevel PrivacyLevel { get; set; } = PrivacyLevel.Public;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Reported { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;

        public virtual ApplicationUserViewModel ApplicationUser { get; set; }
    }
}
