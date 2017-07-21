using ItlaSocial.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ItlaSocial.Models.ApplicationViewModels;

namespace ItlaSocial.Models
{
    public class DbRepository : IDbRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public DbRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<ICollection<Publication>> GetPublicationsAsync(ClaimsPrincipal user)
        {
            List<Publication> publications = new List<Publication>();
            var User = await GetUserAsync(user);

            var Friends = _context.Friendships
                .Where(a => a.ApplicationUser1Id == User.Id && a.Status == FriendshipStatus.Accepted)
                .Select(a => a.ApplicationUser2Id)
                .AsNoTracking()
                .ToList();

            if (_context.Publications.Any(p => p.ApplicationUserId == User.Id))
            {
                foreach (var p in _context.Publications.Where(p => p.ApplicationUserId == User.Id).Select(p => p.Id).ToList())
                {
                    publications.Add(GetPublication(p));
                }
            }

            foreach (var f in Friends)
            {
                if (_context.Publications.Any(p => p.ApplicationUserId == f))
                {
                    foreach (var p in _context.Publications.Where(p => p.ApplicationUserId == f).Select(p => p.Id).ToList())
                    {
                        publications.Add(GetPublication(p));
                    }
                }
            }

            return publications.OrderByDescending(p => p.Date).ToList();
        }



        public ICollection<PublicationMedia> GetPublicationMedia(int publicationId)
        {
            var media = (from m in _context.PublicationMedias
                         where m.PublicationId == publicationId
                         select m)
                         .ToList();
            foreach (var m in media)
            {
                m.MediaUrl = Path.Combine("/", m.MediaUrl).Replace("\\", "/");
            }

            return media;
        }

        public Comment GetComment(int id)
        {
            var comment = (from c in _context.Comments
                           where c.Id == id
                           select c)
                           .Include(c => c.ApplicationUser)
                           .ThenInclude(a => a.ProfilePhotos)
                           .Include(c => c.Likes)
                           .ThenInclude(l => l.ApplicationUser)
                          .First();
            if (comment.ApplicationUser.ProfilePhotos.Where(p => p.Current).Count() == 0)
            {
                comment.ApplicationUser.ProfilePhotos = new List<ProfilePhoto>()
                {
                    new ProfilePhoto()
                    {
                        MediaUrl = "images\\default.png"
                    }
                };
            }
            else
            {
                comment.ApplicationUser.ProfilePhotos = comment.ApplicationUser.ProfilePhotos.Where(p => p.Current).ToList();
            }

            comment.ApplicationUser.ProfilePhotos.First().MediaUrl = Path.Combine("/", comment.ApplicationUser.ProfilePhotos.First().MediaUrl).Replace("\\", "/");

            var comments = new List<Comment>();
            foreach (var com in _context.Comments
                                .Where(c => c.OriginalCommentId == null))
            {
                var coms = _context.Comments
                               .Where(c => c.OriginalCommentId == com.Id)
                               .OrderBy(c => c.Date);
                if (coms.Count() == 0)
                {
                    comments.Add(GetComment(com.Id));
                }
                else
                {
                    comments.Add(GetComment(coms.Last().Id));
                }
            }
            comment.SubComments = comments;

            return comment;
        }

        public Publication GetMediaPublication(int id)
        {
            var publication = (from p in _context.Publications
                               where p.Id == id
                               select p)
                               .Include(p => p.ApplicationUser)
                               .ThenInclude(a => a.ProfilePhotos)
                               .Include(p => p.PublicationMedia)
                           .Include(p => p.Likes)
                           .ThenInclude(l => l.ApplicationUser)
                               .First();

            foreach (var m in publication.PublicationMedia)
            {
                m.MediaUrl = Path.Combine("/", m.MediaUrl).Replace("\\", "/");
            }

            if (publication.ApplicationUser.ProfilePhotos.Where(p => p.Current).Count() == 0)
            {
                publication.ApplicationUser.ProfilePhotos = new List<ProfilePhoto>()
                {
                    new ProfilePhoto()
                    {
                        MediaUrl = "images\\default.png"
                    }
                };
            }
            else
            {
                publication.ApplicationUser.ProfilePhotos = publication.ApplicationUser.ProfilePhotos.Where(p => p.Current).ToList();
            }
            publication.ApplicationUser.ProfilePhotos.First().MediaUrl = Path.Combine("/", publication.ApplicationUser.ProfilePhotos.First().MediaUrl).Replace("\\", "/");

            return publication;
        }

        public Publication GetSharedPublication(int id)
        {
            var sharedPublication = (from p in _context.Publications
                                     where p.Id == id
                                     select p)
                               .Include(p => p.ApplicationUser)
                               .ThenInclude(a => a.ProfilePhotos)
                               .First();

            if (sharedPublication.ApplicationUser.ProfilePhotos.Where(p => p.Current).Count() == 0)
            {
                sharedPublication.ApplicationUser.ProfilePhotos = new List<ProfilePhoto>()
                {
                    new ProfilePhoto()
                    {
                        MediaUrl = "images\\default.png"
                    }
                };
            }
            else
            {
                sharedPublication.ApplicationUser.ProfilePhotos = sharedPublication.ApplicationUser.ProfilePhotos.Where(p => p.Current).ToList();
            }

            sharedPublication.ApplicationUser.ProfilePhotos.First().MediaUrl = Path.Combine("/", sharedPublication.ApplicationUser.ProfilePhotos.First().MediaUrl).Replace("\\", "/");

            return sharedPublication;
        }

        public Publication GetPublication(int id)
        {
            Publication publication;

            if (_context.PublicationMedias.Any(m => m.PublicationId == id))
            {
                publication = GetMediaPublication(id);
            }
            else
            {
                publication = (from p in _context.Publications
                               where (p.Id == id)
                               select p)
                               .Include(p => p.ApplicationUser)
                               .ThenInclude(a => a.ProfilePhotos)
                               .Include(p => p.Likes)
                           .ThenInclude(l => l.ApplicationUser)
                              .First();
                if (publication.ApplicationUser.ProfilePhotos.Where(p => p.Current).Count() == 0)
                {
                    publication.ApplicationUser.ProfilePhotos = new List<ProfilePhoto>()
                {
                    new ProfilePhoto()
                    {
                        MediaUrl = "images\\default.png"
                    }
                };
                }
                else
                {
                    publication.ApplicationUser.ProfilePhotos = publication.ApplicationUser.ProfilePhotos.Where(p => p.Current).ToList();
                }

                publication.ApplicationUser.ProfilePhotos.First().MediaUrl = Path.Combine("/", publication.ApplicationUser.ProfilePhotos.First().MediaUrl).Replace("\\", "/");

                if (publication.SharedPublicationId != null)
                {
                    publication.SharedPublication = GetSharedPublication(publication.SharedPublicationId ?? 0);
                }
            }

            var comments = new List<Comment>();
            foreach (var com in _context.Comments
                                .Where(c => c.OriginalCommentId == null))
            {
                var coms = _context.Comments
                               .Where(c => c.OriginalCommentId == com.Id)
                               .OrderBy(c => c.Date);
                if (coms.Count() == 0)
                {
                    comments.Add(GetComment(com.Id));
                }
                else
                {
                    comments.Add(GetComment(coms.Last().Id));
                }
            }
            publication.Comments = comments;

            if (_context.Publications.Any(p => p.SharedPublicationId == id))
            {
                publication.Shares = _context.Publications.Where(p => p.SharedPublicationId == id)
                    .Include(p => p.ApplicationUser)
                    .ToList();
            }

            return publication;
        }

        public async Task<ICollection<ApplicationUserInvitationViewModel>> FindFriends(string friendName, ClaimsPrincipal userIdentity)
        {
            var result = new List<ApplicationUserInvitationViewModel>();

            var user = await GetUserAsync(userIdentity);
            if (user != null)
            {
                var users = _context.Users
                .Where(u => u.Name.ToLower().Contains(friendName.ToLower())
                || u.LastName.Contains(friendName)
                || String.Concat(u.Name, " ", u.LastName).ToLower().Contains(friendName.ToLower())
                || String.Concat(u.LastName, " ", u.Name).ToLower().Contains(friendName.ToLower()))
                .Include(u => u.ProfilePhotos)
                .ToList();

                if (users.Count() == 0)
                {
                    users = _context.Users
                .Include(u => u.ProfilePhotos)
                .ToList();
                }

                foreach (var u in users)
                {
                    var friend = new ApplicationUserInvitationViewModel();
                    
                    var friendShip = (from f in _context.Friendships
                                      where (f.ApplicationUser1Id == user.Id && f.ApplicationUser2Id == u.Id)
                                      select f);
                    if (friendShip.Count() > 0)
                    {
                        if(friendShip.First().Status == FriendshipStatus.Blocked)
                        {
                            continue;
                        }
                        else
                        {
                            friend.Status = (from f in _context.Friendships
                             where (f.ApplicationUser1Id == u.Id && f.ApplicationUser2Id == user.Id)
                             select f.Status).First();
                        }

                        if (!friendShip.First().Sent)
                        {
                            friend.Sent = true;
                        }
                    }

                    if (u.ProfilePhotos.Where(p => p.Current).Count() > 0)
                    {
                        friend.ProfilePhotos.MediaUrl = u.ProfilePhotos.Where(p => p.Current).First().MediaUrl;
                    }
                    friend.ProfilePhotos.MediaUrl = Path.Combine("/", friend.ProfilePhotos.MediaUrl).Replace("\\", "/");
                    
                    friend.Id = u.Id;
                    friend.Name = u.Name;
                    friend.LastName = u.LastName;

                    result.Add(friend);
                }
            }

            return result;
        }

        public async Task ChangeFriendshipStatus(ClaimsPrincipal user, string friendId, FriendshipStatus status)
        {
            var userId = (await GetUserAsync(user)).Id;

            var userStatus = (from s in _context.Friendships
                              where (s.ApplicationUser1Id == userId
                              && s.ApplicationUser2Id == friendId)
                              select s)
                              .Include(s => s.ApplicationUser1)
                              .Include(s => s.ApplicationUser2);

            var friendStatus = (from s in _context.Friendships
                                where (s.ApplicationUser1Id == friendId
                                && s.ApplicationUser2Id == userId)
                                select s)
                              .Include(s => s.ApplicationUser1)
                              .Include(s => s.ApplicationUser2);

            if (userStatus.Count() > 0 && friendStatus.Count() > 0)
            {
                var uStatus = userStatus.First();
                var fStatus = friendStatus.First();

                if (uStatus.Status != FriendshipStatus.Blocked)
                {
                    if (uStatus.Sent)
                    {
                        switch (uStatus.Status)
                        {
                            case FriendshipStatus.None:
                                switch (status)
                                {
                                    case FriendshipStatus.Pending:
                                        uStatus.Status = FriendshipStatus.Pending;
                                        fStatus.Status = FriendshipStatus.Pending;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;

                                    case FriendshipStatus.None:
                                        fStatus.Status = FriendshipStatus.None;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Cancelled:
                                switch (status)
                                {
                                    case FriendshipStatus.Pending:
                                        uStatus.Status = FriendshipStatus.Pending;
                                        fStatus.Status = FriendshipStatus.Pending;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Accepted:
                                switch (status)
                                {
                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;

                                    case FriendshipStatus.None:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.None;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Declined:
                                switch (status)
                                {
                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Pending:
                                switch (status)
                                {
                                    case FriendshipStatus.Cancelled:
                                        uStatus.Status = FriendshipStatus.Cancelled;
                                        fStatus.Status = FriendshipStatus.Cancelled;
                                        break;
                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (uStatus.Status)
                        {
                            case FriendshipStatus.None:
                                switch (status)
                                {
                                    case FriendshipStatus.Pending:
                                        uStatus.Status = FriendshipStatus.Pending;
                                        fStatus.Status = FriendshipStatus.Pending;
                                        uStatus.Sent = true;
                                        fStatus.Sent = false;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;

                                    case FriendshipStatus.None:
                                        fStatus.Status = FriendshipStatus.None;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Cancelled:
                                switch (status)
                                {
                                    case FriendshipStatus.Pending:
                                        uStatus.Status = FriendshipStatus.Pending;
                                        fStatus.Status = FriendshipStatus.Pending;
                                        uStatus.Sent = true;
                                        fStatus.Sent = false;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Accepted:
                                switch (status)
                                {
                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;

                                    case FriendshipStatus.None:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.None;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Declined:
                                switch (status)
                                {
                                    case FriendshipStatus.Pending:
                                        uStatus.Status = FriendshipStatus.Pending;
                                        fStatus.Status = FriendshipStatus.Pending;
                                        uStatus.Sent = true;
                                        fStatus.Sent = false;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                            case FriendshipStatus.Pending:
                                switch (status)
                                {
                                    case FriendshipStatus.Accepted:
                                        uStatus.Status = FriendshipStatus.Accepted;
                                        fStatus.Status = FriendshipStatus.Accepted;
                                        break;

                                    case FriendshipStatus.Declined:
                                        uStatus.Status = FriendshipStatus.Declined;
                                        fStatus.Status = FriendshipStatus.Declined;
                                        break;

                                    case FriendshipStatus.Blocked:
                                        uStatus.Status = FriendshipStatus.None;
                                        fStatus.Status = FriendshipStatus.Blocked;
                                        break;
                                }
                                break;
                        }
                    }
                }

                _context.Friendships.Update(uStatus);
                _context.Friendships.Update(fStatus);

                await _context.SaveChangesAsync();

            }
            else
            {
                switch (status)
                {
                    case FriendshipStatus.Pending:
                        await AddFriendshipStatus(userId, friendId);
                        break;
                    case FriendshipStatus.Blocked:
                        await AddFriendshipStatus(userId, friendId, true);
                        break;
                }
            }
        }

        public async Task<bool> AddFriendshipStatus(string userId, string friendId, bool block = false)
        {
            try
            {
                _context.Friendships.AddRange(
                    new Friendship()
                    {
                        ApplicationUser1Id = friendId,
                        ApplicationUser2Id = userId,
                        Sent = false,
                        Status = (!block) ? FriendshipStatus.Pending : FriendshipStatus.Blocked
                    },
                     new Friendship()
                     {
                         ApplicationUser1Id = userId,
                         ApplicationUser2Id = friendId,
                         Sent = true,
                         Status = (!block) ? FriendshipStatus.Pending : FriendshipStatus.None
                     }
                );
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            { }
            return false;
        }
    }
}