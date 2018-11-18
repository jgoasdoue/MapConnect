using System;
using System.Data.SqlClient;

namespace ProjetPersoTest.Models
{
    interface IDal: IDisposable
    {
        void OpenDbConn();
        SqlDataReader GetNews();
        SqlDataReader IsUp(string appName);
        SqlDataReader GetLoginInfosDB(string user, string password);
        void CloseDbConn();
    }
}
