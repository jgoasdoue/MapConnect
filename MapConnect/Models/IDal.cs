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
        void OuvrirConnexionBDD(string user, string password);
        SqlDataReader InterrogeBDD();
        void FermerConnexionBDD();
    }
}
