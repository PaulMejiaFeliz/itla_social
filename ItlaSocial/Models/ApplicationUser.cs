using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ItlaSocial.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<ProfilePhoto> ProfilePhotos { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<PublicationLike> PublicationLikes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }

        ICollection<Friendship> _recivedFriends;
        ICollection<Friendship> _friends;

        public virtual ICollection<Friendship> RecivedFriends
        {
            get { return _recivedFriends; }
            set
            {
                _recivedFriends = (from r in value
                                   where (r.ApplicationUser1Id != this.Id && r.Sent)
                                   select (r))
                                  .ToList();
            }
        }
        public virtual ICollection<Friendship> Friends
        {
            get { return _friends; }
            set
            {
                _friends = (from r in value
                            where (r.ApplicationUser1Id == this.Id && r.Sent)
                            select (r))
                                  .ToList();
            }
        }
    }
}
