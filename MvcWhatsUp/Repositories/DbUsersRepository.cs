using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public class DbUsersRepository : IUsersRepository
    {
        private readonly string? _connectionString;

        public DbUsersRepository(IConfiguration configuration)
        {
            //get (database) connection string from appsettings.json    
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }

        private User ReadUser(SqlDataReader reader)
        {
            //retrieve data from fields
            int id = (int)reader["UserId"];
            string name = (string)reader["UserName"];
            string mobileNumber = (string)reader["MobileNumber"];
            string emailAddress = (string)reader["EmailAddress"];
            string password = (string)reader["Password"];
            bool deleted = (bool)reader["Deleted"];

            //return new User object
            return new User(id, name, mobileNumber, emailAddress, password, deleted);
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE Deleted = @deleted";
                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@deleted", false);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }

        public User? GetById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserId = @userId AND Deleted = @deleted";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@deleted", false);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return ReadUser(reader);
                }
                reader.Close();
                return null;
            }
        }

        public void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (UserName, MobileNumber, EmailAddress, Password, Deleted)" +
                    "VALUES (@userName, @mobileNumber, @emailAddress, @password, @deleted)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userName", user.UserName);
                command.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@emailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@deleted", user.Deleted);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users " +
                    "SET UserName = @userName, MobileNumber = @mobileNumber, EmailAddress = @emailAddress, Password = @password " +
                    "WHERE UserID = @userId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userName", user.UserName);
                command.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@emailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@userId", user.UserId);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @userId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userId", userId);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User? GetByLoginCredentials(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserName LIKE @userName AND Password = @password";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@password", password);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return ReadUser(reader);
                }
                reader.Close();
                return null;
            }
        }

        public bool EmailAddressExists(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE EmailAddress = @emailAddress";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
