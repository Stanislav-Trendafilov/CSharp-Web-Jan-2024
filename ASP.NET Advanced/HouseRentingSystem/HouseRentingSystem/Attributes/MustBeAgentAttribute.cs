using HouseRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Controllers;

namespace HouseRentingSystem.Attributes
{
    public class MustBeAgentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            IAgentService? agentService = context.HttpContext.RequestServices.GetService<IAgentService>();

            if (agentService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (agentService != null && agentService.ExistByIdAsync(context.HttpContext.User.Id()).Result==false)
            {
                context.Result = new RedirectToActionResult(nameof(AgentController.Become), "Agent", null);
            }
        }
    }
}
