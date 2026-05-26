using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.API.Controllers;

public abstract class AppControllerBase : ControllerBase
{
    protected Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}