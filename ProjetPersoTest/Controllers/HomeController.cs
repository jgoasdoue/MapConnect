using ProjetPersoTest.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ProjetPersoTest.Controllers
{
    public class HomeController : Controller
    {
        private static bool isConnected;

        private IDal dal = new Dal();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == false)
            {
                return RedirectToAction("Connexion");
            }

            return View("Index");
        }

        public ActionResult Connexion()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == true)
            {
                return RedirectToAction("Index");
            }

            return View("Connexion");
        }

        [HttpPost]
        public ActionResult Connexion(object sender, EventArgs e)
        {
            try
            {
                string user = Request.Form["user"];
                string password = Request.Form["password"];
                dal.OuvrirConnexionBDD(user, password);

                if (user.Contains("'") || user.Contains("\\") || password.Contains("'") || password.Contains("\\"))
                {
                    isConnected = false;
                    ViewData["Message"] = "Tentative d'injection détectée! Annulation de la tentative de connexion";
                    return Connexion();
                }

                SqlDataReader sdr = dal.InterrogeBDD();

                if ((sdr.Read() == true))
                {
                    ViewData["Message"] = "";
                    isConnected = true;
                }
                else
                {
                    ViewData["Message"] = "Association utilisateur/mot de passe non reconnue. Veuillez réessayer";
                    isConnected = false;
                }
            }
            finally
            {
                dal.FermerConnexionBDD();
                ViewBag.isConnected = isConnected;
            }

            return Connexion();
        }

        public ActionResult Deconnexion()
        {
            isConnected= false;
            ViewBag.isConnected = isConnected;
            return View("Deconnexion");
        }

        public ActionResult Map()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == true)
            {
                return View("Map");
            }
            return RedirectToAction("Connexion");
        }
    }
}