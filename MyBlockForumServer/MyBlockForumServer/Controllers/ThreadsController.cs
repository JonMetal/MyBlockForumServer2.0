using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.Controllers.RequestProceccor;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Services;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ThreadsController(IThreadService threadService) : Controller
    {
        private readonly IThreadService _threadService = threadService;

        [HttpGet("AllThreads", Name = "GetThreads")]
        public async Task<IEnumerable<ThreadDto>> GetThreadsAsync()
        {
            return await _threadService.GetAllThreadsAsync();
        }

        [HttpGet("AllThreadThemes", Name = "GetThreadThemes")]
        public async Task<IEnumerable<ThreadThemeDto>> GetThreadThemesAsync()
        {
            return await _threadService.GetAllThreadThemesAsync();
        }

        [HttpGet("{id}", Name = "GetThreadById")]
        public async Task<ActionResult<ThreadDto>> GetThreadAsync(Guid id)
        {
            return await RequestHandler<ThreadDto>.ProcessingGetByGuidRequest(this, _threadService.GetThreadAsync, id);
        }

        [HttpGet("ThreadThemes/{id}")]
        public async Task<ActionResult<ThreadThemeDto>> GetThreadThemeAsync(Guid id)
        {
            return await RequestHandler<ThreadThemeDto>.ProcessingGetByGuidRequest(this, _threadService.GetThreadThemeAsync, id);
        }

        [HttpGet("ThreadsByTheme/{id}")]
        public async Task<ActionResult<IEnumerable<ThreadDto>>> GetThreadsByThemeAsync(Guid id)
        {
            return await RequestHandler<IEnumerable<ThreadDto>>.
                ProcessingGetByGuidRequest(this, _threadService.GetThreadsByThemeAsync, id);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ThreadDto thread)
        {
            if (thread == null) { return BadRequest(); }
            return Ok(await _threadService.CreateThreadAsync(thread));
        }

        [HttpPut]
        public async Task<ActionResult<Thread>> PutAsync(ThreadDto thread)
        {
            if (thread == null) { return BadRequest(); }
            await _threadService.UpdateThreadAsync(thread);
            return Ok(thread);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _threadService.DeleteThreadAsync(id);
            return Ok();
        }
    }
}
