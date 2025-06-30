
using Microsoft.Extensions.Configuration;

using Microsoft.Data.SqlClient;

namespace finance.data
{
    public static class DbConnectionHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=LAPTOP-MEERAH74\\SQLEXPRESS;Database=FINANCEDB;Trusted_Connection=True;;TrustServerCertificate=True;";
            return new SqlConnection(connectionString);
        }
    }
}



