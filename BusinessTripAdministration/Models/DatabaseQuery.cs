using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripAdministration.Models
{
    public static class DatabaseQuery
    {
        private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True";
        private static readonly SqlConnection sqlConnection = new SqlConnection(connectionString);

      

        public static bool Login(string email, string password)
        {
            var cmd = new SqlCommand
            {
                CommandText = "SELECT * FROM Users WHERE Email=" + email + " AND Password=" + Crypto.Hash(password),
                CommandType = CommandType.Text,
                Connection = sqlConnection
            };

            sqlConnection.Open();

            var reader = cmd.ExecuteReader();
            var fieldCount = reader.FieldCount;

            sqlConnection.Close();
            return true;
        }
    }
}
