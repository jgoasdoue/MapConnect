using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetPersoTest.Models
{
    public class Dal: IDal
    {
        private ConnInfos infos;

        public Dal()
        {
            infos = new ConnInfos
            {
                Con = new SqlConnection(),
                Login = "",
                Password = ""
            };
        }

        public void OuvrirConnexionBDD(string user, string password)
        {
            infos = new ConnInfos {
                Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jgoasdoue\Documents\dbTest.mdf;Integrated Security=True;Connect Timeout=30"),
                Login = user,
                Password = password
            };
            infos.Con.Open();
        }

        public SqlDataReader InterrogeBDD()
        {
            if(infos.Con == null)
            {
                throw new Exception("La connexion à la base n'est pas initialisée");
            }

            string request = "SELECT * FROM Login WHERE [user] = '" + infos.Login + "' AND [pass] = '" + infos.Password + "' ";
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public void FermerConnexionBDD()
        {
            if(infos.Con != null)
            {
                infos.Con.Close();
            }

            infos.Login = infos.Password = null;
        }

        public void Dispose()
        {
            infos.Con.Dispose();
            infos.Login = null;
        }
    }
}