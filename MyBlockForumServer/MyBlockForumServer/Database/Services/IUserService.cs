using MyBlockForumServer.Database.Models;

namespace MyBlockForumServer.Database.Services
{
    public interface IUserService
    {
        Task<bool> AddKarmaAsync(Guid fromUserId, Guid toUserId);
        Task AddThreadUserAsync(Guid userId, Guid threadId);
        Task<UserRegistryDto> CreateUserAsync(UserRegistryDto user);
        Task DeleteUserAsync(Guid id);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
        Task<UserDto> GetUserAsync(Guid id);
        Task<UserDto> GetUserByLoginAsync(string login);
        Task<RoleDto> GetUserRoleAsync(Guid id);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<IEnumerable<UserDto>> GetUsersFromThreadAsync(Guid threadId);
        Task<StatusDto> GetUserStatusAsync(Guid id);
        Task<IEnumerable<ThreadDto>> GetUserThreadsAsync(Guid id);
        Task<string> LoginAsync(string login, string password);
        Task<bool> RemoveVoteKarmaAsync(Guid fromUserId, Guid toUserId);
        Task SetUserAsync(UserDto user);
    }
}