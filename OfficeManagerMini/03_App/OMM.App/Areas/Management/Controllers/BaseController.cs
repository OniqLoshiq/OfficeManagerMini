using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Admin, Management, HR")]
    public abstract class BaseController : Controller
    { 
    }
}