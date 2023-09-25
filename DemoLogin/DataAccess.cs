using System;
using System.Data;
using System.Data.SqlClient;

public static class DataAccess
{
    private static string connectionString = @"data source=DIVYAKAR\SQLEXPRESS; database=Divdb; Integrated Security=true";

    public static bool CheckLogin(string username, string password)
    {
        bool isValidUser = false;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isValidUser = (count > 0);
            }
            catch (Exception ex)
            {
                // Handle exception if needed
            }
        }

        return isValidUser;
    }
}
