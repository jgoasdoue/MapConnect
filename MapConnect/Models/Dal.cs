using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjetPersoTest.Models
{
    public class Dal: IDal
    {
        private readonly ConnInfos infos;

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

        private SqlDataReader GetDataFromRequest(string request)
        {
            OpenDbConn();

            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();
            return result;
        }

        public SqlDataReader GetLoginInfosDB(string user, string password)
        {
            infos.Login = user;
            infos.Password = password;

            if (infos.Con.ConnectionString == "")
            {
                throw new InvalidOperationException(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }
            string request = String.Format(ConfigurationManager.AppSettings["loginRequest"], infos.Login, infos.Password);
            return GetDataFromRequest(request);
        }

        public SqlDataReader GetNews()
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new InvalidOperationException(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }

            string request = ConfigurationManager.AppSettings["newsRequest"];
            return GetDataFromRequest(request);
        }

        public SqlDataReader IsUp(string appName)
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new InvalidOperationException(ConfigurationManager.AppSettings["errorNullConnectString"]);
            }

            string request = String.Format(ConfigurationManager.AppSettings["maintenanceRequest"], appName);
            return GetDataFromRequest(request);
        }

        public void CloseDbConn()
        {
            if(infos.Con.ConnectionString != null)
            {
                infos.Con.Close();
            }

            infos.Login = null;
            infos.Password = null;
        }

        void IDisposable.Dispose()
        {
            infos.Con.Dispose();
            infos.Login = null;
        }
    }
}