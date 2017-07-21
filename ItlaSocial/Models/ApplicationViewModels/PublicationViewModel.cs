using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class PublicationViewModel
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

        public virtual ICollection<PublicationViewModel> Shares { get; set; }

        public virtual ICollection<PublicationLike> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Publication SharedPublication { get; set; }

        public virtual ICollection<PublicationMediaViewModel> PublicationMedia { get; set; }


    }
}