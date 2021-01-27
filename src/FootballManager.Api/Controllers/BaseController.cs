using FootballManager.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    public abstract class BaseController : ControllerBase
    {
        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; set; }

        protected ActionResult ResolveResult<T>(T notificationModel) where T : NotificationViewModel
        {
            if (notificationModel.Notifications.HasErrors())
            {
                return new ObjectResult(notificationModel.Notifications)
                {
                    StatusCode = 500
                };
            }

            if (notificationModel.Notifications.HasWarnings())
            {
                return BadRequest(notificationModel.Notifications);
            }

            return Ok(notificationModel);
        }
    }
}
