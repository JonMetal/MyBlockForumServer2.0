using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.Auth;
using MyBlockForumServer.Controllers.RequestProceccor;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Services;

namespace MyBlockForumServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            return await RequestHandler<UserDto>.
                ProcessingGetByGuidRequest(this, _userService.GetUserAsync, id);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Login([FromBody] LoginRequestDetails loginRequestDetails)
        {
            try
            {
                var token = await _userService.LoginAsync(loginRequestDetails.Login, loginRequestDetails.Password);

                CookieOptions cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                };
                UserDto user = await _userService.GetUserByLoginAsync(loginRequestDetails.Login);
                string id = user.Id.ToString();
                HttpContext.Response.Cookies.Append("snezhok_cookie", token, cookieOptions);
                HttpContext.Response.Cookies.Append("user_cookie", id);
                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpGet("GetIdByLogin")]
        public async Task<ActionResult<Guid>> GetByLogin(string Login)
        {
            UserDto user = await _userService.GetUserByLoginAsync(Login);
            return Ok(user.Id);
        }

        [HttpGet("AllStatuses")]
        [AllowAnonymous]
        public async Task<IEnumerable<StatusDto>> GetAllStatuses()
        {
            return await _userService.GetAllStatusesAsync();
        }

        [HttpGet("AllRoles")]
        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            return await _userService.GetAllRolesAsync();
        }

        [HttpGet("UsersFromThread/{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersFromThreadAsync(Guid id)
        {
            return await RequestHandler<IEnumerable<UserDto>>.
                ProcessingGetByGuidRequest(this, _userService.GetUsersFromThreadAsync, id);
        }

        [HttpGet("UserRole/{id}")]
        public async Task<ActionResult<RoleDto>> GetUserRoleAsync(Guid id)
        {

            return await RequestHandler<RoleDto>.
                ProcessingGetByGuidRequest(this, _userService.GetUserRoleAsync, id);
        }

        [HttpGet("UserThreads/{id}")]
        public async Task<ActionResult<IEnumerable<ThreadDto>>> GetUserThreads(Guid id)
        {
            return await RequestHandler<IEnumerable<ThreadDto>>.
                ProcessingGetByGuidRequest(this, _userService.GetUserThreadsAsync, id);
        }

        [HttpGet("UserStatus/{id}")]
        public async Task<ActionResult<StatusDto>> GetUserStatus(Guid id)
        {
            return await RequestHandler<StatusDto>.
                ProcessingGetByGuidRequest(this, _userService.GetUserStatusAsync, id);
        }

        [HttpPost("UserThread/{userId}/{threadId}")]
        public async Task<ActionResult> AddUserThread(Guid userId, Guid threadId)
        {
            IEnumerable<ThreadDto> threads = await _userService.GetUserThreadsAsync(userId);
            if (threads.FirstOrDefault(l => l.Id == threadId) == null)
            {
                await _userService.AddThreadUserAsync(userId, threadId);
            }
            else
            {
                return BadRequest("User already is joined to thread");
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserRegistryDto>> CreateUserAsync(UserRegistryDto user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> SetUserAsync(UserDto user)
        {
            if (user == null) { return BadRequest(); }
            await _userService.SetUserAsync(user);
            return Ok(user);
        }

        [HttpPut("UpdateStatus/{userId}/{statusId}")]
        public async Task<ActionResult<User>> SetStatusAsync(Guid userId, Guid statusId)
        {
            try
            {
                UserDto user = await _userService.GetUserAsync(userId);
                user.StatusId = statusId;
                await _userService.SetUserAsync(user);
                return Ok(user);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            HttpContext.Response.Cookies.Delete("snezhok_cookie");
            HttpContext.Response.Cookies.Delete("user_cookie");
            return Ok();
        }

        [HttpPost("AddKarmaAsync/{fromUserId}/{toUserid}")]
        public async Task<ActionResult<bool>> AddKarmaAsync(Guid fromUserId, Guid toUserid)
        {
            bool result = await _userService.AddKarmaAsync(fromUserId, toUserid);
            return Ok(result);
        }

        [HttpDelete("RemoveVoteKarmaAsync/{fromUserId}/{toUserid}")]
        public async Task<ActionResult<bool>> RemoveVoteKarmaAsync(Guid fromUserId, Guid toUserid)
        {
            bool result = await _userService.RemoveVoteKarmaAsync(fromUserId, toUserid);
            return Ok(result);
        }
    }
}
