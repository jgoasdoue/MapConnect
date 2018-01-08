using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjetPersoTest
{
    public class RouteConfig
    {
        /**
         * Définit la route par défaut à utiliser quand l'action n'est pas renseignée, ou quand l'utilisateur accède à localhost:port
         * Définit les routes à ignorer
         */
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Connexion", id = UrlParameter.Optional }
            );
        }
    }
}
