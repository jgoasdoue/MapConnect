using System;
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
                Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jgoasdoue\Documents\dbTest.mdf;Integrated Security=True;Connect Timeout=30"),
                Login = "",
                Password = ""
            };
        }

        public void OpenDBConn()
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
                throw new Exception("La connexion à la base n'est pas initialisée");
            }

            OpenDBConn();

            string request = "SELECT * FROM Login WHERE [user] = '" + infos.Login + "' AND [pass] = '" + infos.Password + "' ";
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public SqlDataReader GetNews()
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new Exception("La connexion à la base n'est pas initialisée");
            }

            OpenDBConn();

            string request = "SELECT News_date, Content FROM News ORDER BY News_date DESC";
            SqlCommand cmd = new SqlCommand(request, infos.Con);
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }

        public SqlDataReader IsUp(string appName)
        {
            if (infos.Con.ConnectionString == "")
            {
                throw new Exception("La connexion à la base n'est pas initialisée");
            }

            OpenDBConn();

            string request = "SELECT State, Message FROM Maintenance WHERE Name = '" + appName + "'";
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