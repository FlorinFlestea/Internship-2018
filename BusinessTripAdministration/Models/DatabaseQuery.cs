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
            string commandText = "SELECT * FROM Users WHERE Email= @email "
                                 + "AND Password= @pass;";
            SqlParameter paramEmail = new SqlParameter();
            paramEmail.ParameterName = "@email";
            paramEmail.Value = email;

            SqlParameter paramPass = new SqlParameter();
            paramPass.ParameterName = "@pass";
            paramPass.Value = Crypto.Hash(password);

            SqlCommand cmd = new SqlCommand(
                commandText, sqlConnection);
            cmd.Parameters.Add(paramEmail);
            cmd.Parameters.Add(paramPass);

            sqlConnection.Open();

            var reader = cmd.ExecuteReader();
            var returnOption = reader.HasRows;
            sqlConnection.Close();
            if (returnOption)
                return CheckIfAdmin(email);
            return returnOption;
        }

        public static bool CheckIfAdmin(string email)
        {
            return true;
        }
    }
}
