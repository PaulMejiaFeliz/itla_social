using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Reported { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int PublicationId { get; set; }
        public virtual Publication Publication { get; set; }

        public int? OriginalCommentId { get; set; }
        [ForeignKey("OriginalCommentId"), InverseProperty("Edits")]
        public virtual Comment OriginalComment { get; set; }

        public int? SuperCommentId { get; set; }
        [ForeignKey("SuperCommentId"), InverseProperty("SubComments")]
        public virtual Comment SuperComment { get; set; }

        public virtual ICollection<CommentLike> Likes { get; set; }

        public virtual ICollection<Comment> Edits { get; set; }

        public virtual ICollection<Comment> SubComments { get; set; }
    }
}
