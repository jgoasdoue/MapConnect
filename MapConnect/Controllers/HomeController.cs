using Newtonsoft.Json;
using ProjetPersoTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ProjetPersoTest.Controllers
{
    public class HomeController : Controller
    {
        private static bool isConnected;

        private IDal dal = new Dal();

        /**
         * Permet d'afficher la page Index.cshtml si l'utilisateur est connecté, sinon renvoie sur la page de connexion (Connexion.cshtml)
         */
        public ActionResult Index()
        {
            try
            {
                SqlDataReader maintenanceResult = dal.IsUp("Home");
                if (maintenanceResult.HasRows)
                {
                    while (maintenanceResult.Read())
                    {
                        if (maintenanceResult.GetInt32(0) == 1)
                        {
                            isConnected = false;
                            Session["message"] = maintenanceResult.GetString(1);
                            return RedirectToAction("Index", "Maintenance");
                        }
                    }
                    maintenanceResult.Close();
                }

                ViewBag.isConnected = isConnected;

                ViewBag.News = new List<string>();
                SqlDataReader newsResult = dal.GetNews();

                if (newsResult.HasRows)
                {
                    while (newsResult.Read())
                    {
                        ViewBag.News.Add(newsResult.GetString(1));
                    }
                    newsResult.Close();
                }
            }
            catch (Exception e)
            {
                isConnected = false;
                ViewData["Message"] = String.Format(ConfigurationManager.AppSettings["generalErrorString"], e.Message);
                dal.CloseDbConn();
            }

            if (isConnected == false)
            {
                return RedirectToAction("Connexion");
            }

            return View("Index");
        }

        /**
         * Permet d'afficher la page de connexion (Connexion.cshtml) si l'utilisateur est déconnecté, sinon renvoie sur la page Index.cshtml
         */
        public ActionResult Connexion()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == true)
            {
                return RedirectToAction("Index");
            }

            return View("Connexion");
        }

        /**
         * Vérifie le contenu des champs user et password entrés par l'utilisateur et les compare avec la base pour connecter ou non l'utilisateur
         */
        [HttpPost]
        public ActionResult Connexion(object sender, EventArgs e)
        {
            String user = Request.Form["user"];
            String password = Request.Form["password"];
            CaptchaResponse captchaResponse = new CaptchaResponse();
            if ((user != null && password != null))
            {
                String response = Request["g-recaptcha-response"];
                String verifLink = String.Format(ConfigurationManager.AppSettings["verifLinkGoogle"], ConfigurationManager.AppSettings["privateCaptchaKey"], response);
                WebClient client = new WebClient();

                if (WebConfigurationManager.AppSettings["AppContext"].ToUpper() != "PROXYLESS")
                {
                    WebProxy proxy = new WebProxy(ConfigurationManager.AppSettings["proxyAddrValue"] + ":8080", true);
                    proxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["proxyUserNameValue"], ConfigurationManager.AppSettings["proxyPassWordValue"]);
                    client.Proxy = proxy;
                }

                var reply = client.DownloadString(verifLink);
                captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            }

            if (user.Contains("'") || user.Contains("\\") || password.Contains("'") || password.Contains("\\"))
            {
                isConnected = false;
                ViewData["Message"] = "Tentative d'injection détectée! Annulation de la tentative de connexion";
                return Connexion();
            }
            try
            {
                SqlDataReader sdr = dal.GetLoginInfosDB(user, password);

                if ((sdr.Read() == true))
                {
                    if (captchaResponse.Success == "false"){
                        ViewData["Message"] = "Captcha wasn't ticked";
                        isConnected = false;
                    }
                    else {
                        ViewData["Message"] = "";
                        isConnected = true;
                    }
                }
                else
                {
                    ViewData["Message"] = "Association utilisateur/mot de passe non reconnue. Veuillez réessayer";
                    isConnected = false;
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                ViewData["Message"] = String.Format(ConfigurationManager.AppSettings["generalErrorString"], ex.Message);
            }
            finally
            {
                dal.CloseDbConn();
                ViewBag.isConnected = isConnected;
            }

            return Connexion();
        }

        /**
         * Permet de se déconnecter et de revenir sur la page de connexion (Connexion.cshtml)
         */
        public ActionResult Deconnexion()
        {
            isConnected= false;
            ViewBag.isConnected = isConnected;
            dal.CloseDbConn();
            return View("Deconnexion");
        }

        /**
         * Permet d'afficher la page Map.cshtml si l'utilisateur est connecté, sinon renvoie sur la page de connexion (Connexion.cshtml)
         */
        public ActionResult Map()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == true)
            {
                return View("Map");
            }
            return RedirectToAction("Connexion");
        }

        /**
         * Permet d'afficher la page About.cshtml si l'utilisateur est connecté, sinon renvoie sur la page de connexion (Connexion.cshtml)
         */
        public ActionResult About()
        {
            ViewBag.isConnected = isConnected;
            if (isConnected == true)
            {
                return View("About");
            }
            return RedirectToAction("Connexion");
        }
    }
}