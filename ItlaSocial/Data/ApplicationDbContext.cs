using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ItlaSocial.Models;

namespace ItlaSocial.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationLike> PublicationLikes { get; set; }
        public DbSet<PublicationMedia> PublicationMedias { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ItlaSocial.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
