using ItlaSocial.Models.ApplicationViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItlaSocial.Models
{
    public interface IDbRepository
    {
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user);

        Task<ICollection<Publication>> GetPublicationsAsync(ClaimsPrincipal user);


        Publication GetPublication(int id);
        Publication GetSharedPublication(int id);
        Publication GetMediaPublication(int id);

        ICollection<PublicationMedia> GetPublicationMedia(int publicationId);

        Comment GetComment(int id);

        Task<ICollection<ApplicationUserInvitationViewModel>> FindFriends(string friendName, ClaimsPrincipal userIdentity);

        Task ChangeFriendshipStatus(ClaimsPrincipal user, string friendId, FriendshipStatus status);

        Task<bool> AddFriendshipStatus(string userId, string friendId, bool block = false);
    }
}