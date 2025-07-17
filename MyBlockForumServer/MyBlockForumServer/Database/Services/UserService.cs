using Mapster;
using MyBlockForumServer.Auth;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Repositories;
using MyBlockForumServer.Tools;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer.Database.Services
{
    public class UserService(IRepository<Role> roleRepository,
        IRepository<Status> statusRepository,
        IRepository<User> userRepository,
        IRepository<Thread> threadRepository,
        IJwtProvider jwtProvider) : IUserService
    {
        readonly IRepository<Thread> _threadRepository = threadRepository;
        readonly IRepository<Role> _roleRepository = roleRepository;
        readonly IRepository<Status> _statusRepository = statusRepository;
        readonly IRepository<User> _userRepository = userRepository;
        readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            User? user = await _userRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(user);
            return user.Adapt<UserDto>();
        }

        public async Task<UserDto> GetUserByLoginAsync(string login)
        {
            User? user = await _userRepository.GetAsync(l => l.Login == login);
            ArgumentNullException.ThrowIfNull(user);
            return user.Adapt<UserDto>();
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            return users.Select(u => u.Adapt<UserDto>());
        }

        public async Task<string> LoginAsync(string login, string password)
        {
            User? user = await _userRepository.GetAsync(l => l.Login == login);
            ArgumentNullException.ThrowIfNull(user);
            if (Hash.GetHash(login, password) != user.Password)
            {
                throw new Exception("Failed to Login");
            }

            return _jwtProvider.GenerateToken(user);
        }

        public async Task<UserRegistryDto> CreateUserAsync(UserRegistryDto userDto)
        {
            User user = userDto.Adapt<User>();
            user.Password = Hash.GetHash(user.Login ?? "", user.Password ?? "");
            Status? status = await _statusRepository.GetAsync(l => true);
            ArgumentNullException.ThrowIfNull(status);
            user.StatusId = status.Id;
            Role? role = await _roleRepository.GetAsync(l => true);
            ArgumentNullException.ThrowIfNull(role);
            user.RoleId = role.Id;
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveAsync();
            return userDto;
        }

        public async Task SetUserAsync(UserDto user)
        {
            await _userRepository.UpdateAsync(user.Adapt<User>());
            await _userRepository.SaveAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveAsync();
        }

        public async Task<RoleDto> GetUserRoleAsync(Guid id)
        {
            Role? role = await _roleRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(role);
            return role.Adapt<RoleDto>();
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            IEnumerable<Role> roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => r.Adapt<RoleDto>());
        }

        public async Task<StatusDto> GetUserStatusAsync(Guid id)
        {
            User? user = await _userRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(user);
            Status? status = await _statusRepository.GetAsync(user.StatusId);
            ArgumentNullException.ThrowIfNull(status);
            return status.Adapt<StatusDto>();
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            IEnumerable<Status> statuses = await _statusRepository.GetAllAsync();
            return statuses.Select(s => s.Adapt<StatusDto>());
        }

        public async Task AddThreadUserAsync(Guid userId, Guid threadId)
        {
            User? user = await _userRepository.GetAsync(l => l.Id == userId);
            Thread? thread = await _threadRepository.GetAsync(threadId);
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(thread);
            user.Threads.Add(thread);
            await _userRepository.SaveAsync();
        }

        public async Task<IEnumerable<ThreadDto>> GetUserThreadsAsync(Guid id)
        {
            User? user = await _userRepository.GetAsync(l => l.Id == id);
            ArgumentNullException.ThrowIfNull(user);
            return user.Threads.Select(t => t.Adapt<ThreadDto>());
        }

        public async Task<bool> AddKarmaAsync(Guid fromUserId, Guid toUserId)
        {
            User? userFrom = await _userRepository.GetAsync(fromUserId);
            User? userTo = await _userRepository.GetAsync(toUserId);
            if (userFrom != null && userTo != null)
            {
                if (userTo.FromUsers.Contains(userFrom)) { return false; }
                userTo.Karma += 1;
                userTo.FromUsers.Add(userFrom);
                await _userRepository.SaveAsync();
                return true;
            }
            else
            {
                throw new Exception("One of users is not defined");
            }
        }

        public async Task<bool> RemoveVoteKarmaAsync(Guid fromUserId, Guid toUserId)
        {
            User? userFrom = await _userRepository.GetAsync(fromUserId);
            User? userTo = await _userRepository.GetAsync(toUserId);
            if (userFrom != null && userTo != null)
            {
                if (!userTo.FromUsers.Contains(userFrom)) { return false; }
                userTo.Karma -= 1;
                userTo.FromUsers.Remove(userFrom);
                await _userRepository.SaveAsync();
                return true;
            }
            else
            {
                throw new Exception("One of users is not defined");
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsersFromThreadAsync(Guid threadId)
        {
            Thread? thread = await _threadRepository.GetAsync(threadId);
            ArgumentNullException.ThrowIfNull(thread);
            return thread.Users.Select(u => u.Adapt<UserDto>());
        }
    }
}
