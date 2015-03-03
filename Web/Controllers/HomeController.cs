using System.Web.Mvc;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace SignalR.WebApi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            ViewBag.Role = GetRoleId;
            return View();
        }


        private static string GetRoleId 
        {
            get
            {
                if (RoleEnvironment.IsAvailable)
                {
                    return RoleEnvironment.CurrentRoleInstance.Id;
                }
                return "Local";
            }
        }
    }
}