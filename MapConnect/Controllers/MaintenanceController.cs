using System.Web.Mvc;

namespace ProjetPersoTest.Controllers
{
    public class MaintenanceController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = Session["message"];
            return View();
        }
    }
}