using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjetPersoTest.Models
{
    public class Dal: IDal
    {
        private ConnInfos infos;

        public Dal()
        {
            infos = new ConnInfos
            {
                Con = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]),
                Login = "",
                Password = ""
            };
        }

        public void OpenDbConn()
        {
            if(infos.Con.State == ConnectionState.Closed)
            {
                infos.Con.Open();
            }
        }

        public SqlDataReader GetLoginInfosDB(string user, string password)
        {
            infos.Login = user;
            infos.Password = password;

            if (infos.Con.ConnectionString == "")
            {
                throw new Exception(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }

            OpenDbConn();

            string request = String.Format(ConfigurationManager.AppSettings["loginRequest"], infos.Login, infos.Password);// "SELECT * FROM Login WHERE [user] = '" + infos.Login + "' AND [pass] = '" + infos.Password + "' ";
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public SqlDataReader GetNews()
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new Exception(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }

            OpenDbConn();

            string request = ConfigurationManager.AppSettings["newsRequest"];
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public SqlDataReader IsUp(string appName)
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new Exception(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }

            OpenDbConn();

            string request = String.Format(ConfigurationManager.AppSettings["maintenanceRequest"], appName);
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public void CloseDBConn()
        {
            if(infos.Con.ConnectionString != null)
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