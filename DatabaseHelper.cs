using System;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    private static string connectionString = "Server=BELLE\\SQLEXPRESS;Database=TaskManager;Trusted_Connection=True;";

    public static void ExecuteQuery(string query, Action<SqlCommand> parameterize = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                parameterize?.Invoke(cmd);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static SqlDataReader ExecuteReader(string query, Action<SqlCommand> parameterize = null)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            parameterize?.Invoke(cmd);
            return cmd.ExecuteReader();
        }
    }
}
