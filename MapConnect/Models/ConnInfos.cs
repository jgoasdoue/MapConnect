using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace ProjetPersoTest.Models
{
    public class ConnInfos
    {
        [Required]
        public SqlConnection Con { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}