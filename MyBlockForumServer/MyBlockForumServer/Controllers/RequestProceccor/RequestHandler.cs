using Microsoft.AspNetCore.Mvc;

namespace MyBlockForumServer.Controllers.RequestProceccor
{
    public static class RequestHandler<T> where T : class
    {
        public async static Task<ActionResult<T>> ProcessingListRequest(ControllerBase controller,
            Func<Task<T>> func)
        {
            try
            {
                return controller.Ok(await func());
            }
            catch (ArgumentNullException ex)
            {
                return controller.NotFound(ex);
            }
            catch
            {
                return controller.BadRequest();
            }
        }

        public async static Task<ActionResult<T>> ProcessingGetByGuidRequest(ControllerBase controller,
            Func<Guid, Task<T>> func,
            Guid id)
        {
            try
            {
                return controller.Ok(await func(id));
            }
            catch (ArgumentNullException ex)
            {
                return controller.NotFound(ex);
            }
            catch
            {
                return controller.BadRequest();
            }
        }
    }
}
