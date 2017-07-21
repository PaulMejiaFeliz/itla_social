using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models
{
    public class Publication
    {
        [Key]
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

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? OriginalPublicationId { get; set; }
        [ForeignKey("OriginalPublicationId"), InverseProperty("Edits")]
        public virtual Publication OriginalPublication { get; set; }

        public int? SharedPublicationId { get; set; }
        [ForeignKey("SharedPublicationId"), InverseProperty("Shares")]
        public virtual Publication SharedPublication { get; set; }

        public virtual ICollection<PublicationMedia> PublicationMedia { get; set; }

        public virtual ICollection<Publication> Shares { get; set; }

        public virtual ICollection<Publication> Edits { get; set; }

        public virtual ICollection<PublicationLike> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
