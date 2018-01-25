using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
