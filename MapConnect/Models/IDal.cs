using System;
using System.Data.SqlClient;

namespace ProjetPersoTest.Models
{
    interface IDal: IDisposable
    {
        void OpenDBConn();
        SqlDataReader GetNews();
        SqlDataReader IsUp(string appName);
        SqlDataReader GetLoginInfosDB(string user, string password);
        void CloseDBConn();
    }
}
