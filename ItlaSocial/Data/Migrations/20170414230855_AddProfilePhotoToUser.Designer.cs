using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ItlaSocial.Data;
using ItlaSocial.Models;

namespace ItlaSocial.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170414230855_AddProfilePhotoToUser")]
    partial class AddProfilePhotoToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ItlaSocial.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ItlaSocial.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int?>("OriginalCommentId");

                    b.Property<int>("PublicationId");

                    b.Property<bool>("Reported");

                    b.Property<int?>("SuperCommentId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("OriginalCommentId");

                    b.HasIndex("PublicationId");

                    b.HasIndex("SuperCommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ItlaSocial.Models.CommentLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("CommentId");

                    b.Property<int?>("CommentId1");

                    b.Property<bool>("DisLike");

                    b.Property<bool>("UnLike");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CommentId1");

                    b.ToTable("CommentLikes");
                });

            modelBuilder.Entity("ItlaSocial.Models.Friendship", b =>
                {
                    b.Property<string>("ApplicationUser1Id");

                    b.Property<string>("ApplicationUser2Id");

                    b.Property<bool>("Sent");

                    b.Property<int>("Status");

                    b.HasKey("ApplicationUser1Id", "ApplicationUser2Id");

                    b.HasIndex("ApplicationUser2Id");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ItlaSocial.Models.ProfilePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AplicationUserId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<bool>("Current");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("MediaUrl")
                        .IsRequired();

                    b.Property<int>("PrivacyLevel");

                    b.Property<bool>("Reported");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ProfilePhotos");
                });

            modelBuilder.Entity("ItlaSocial.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int?>("OriginalPublicationId");

                    b.Property<int>("PrivacyLevel");

                    b.Property<bool>("Reported");

                    b.Property<int?>("SharedPublicationId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("OriginalPublicationId");

                    b.HasIndex("SharedPublicationId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("ItlaSocial.Models.PublicationLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<bool>("DisLike");

                    b.Property<string>("PublicationId");

                    b.Property<int?>("PublicationId1");

                    b.Property<bool>("UnLike");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PublicationId1");

                    b.ToTable("PublicationLikes");
                });

            modelBuilder.Entity("ItlaSocial.Models.PublicationMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("MediaType");

                    b.Property<string>("MediaUrl")
                        .IsRequired();

                    b.Property<int>("PublicationId");

                    b.Property<bool>("Reported");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("PublicationMedias");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ItlaSocial.Models.Comment", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ItlaSocial.Models.Comment", "OriginalComment")
                        .WithMany("Edits")
                        .HasForeignKey("OriginalCommentId");

                    b.HasOne("ItlaSocial.Models.Publication", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItlaSocial.Models.Comment", "SuperComment")
                        .WithMany("SubComments")
                        .HasForeignKey("SuperCommentId");
                });

            modelBuilder.Entity("ItlaSocial.Models.CommentLike", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("CommentLikes")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ItlaSocial.Models.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentId1");
                });

            modelBuilder.Entity("ItlaSocial.Models.Friendship", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser1")
                        .WithMany("RecivedFriends")
                        .HasForeignKey("ApplicationUser1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser2")
                        .WithMany("Friends")
                        .HasForeignKey("ApplicationUser2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItlaSocial.Models.ProfilePhoto", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("ProfilePhoto")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("ItlaSocial.Models.Publication", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Publications")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ItlaSocial.Models.Publication", "OriginalPublication")
                        .WithMany("Edits")
                        .HasForeignKey("OriginalPublicationId");

                    b.HasOne("ItlaSocial.Models.Publication", "SharedPublication")
                        .WithMany("Shares")
                        .HasForeignKey("SharedPublicationId");
                });

            modelBuilder.Entity("ItlaSocial.Models.PublicationLike", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("PublicationLikes")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ItlaSocial.Models.Publication", "Publication")
                        .WithMany("Likes")
                        .HasForeignKey("PublicationId1");
                });

            modelBuilder.Entity("ItlaSocial.Models.PublicationMedia", b =>
                {
                    b.HasOne("ItlaSocial.Models.Publication", "Publication")
                        .WithMany("PublicationMedia")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ItlaSocial.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItlaSocial.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
