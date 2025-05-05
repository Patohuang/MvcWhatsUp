using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public class DbChatRepository : IChatsRepository
    {
        private readonly string? _connectionString;

        public DbChatRepository(IConfiguration configuration)
        {
            //get (database) connection string from appsettings.json    
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }

        private Message ReadMessage(SqlDataReader reader)
        {
            //retrieve data from fields
            int messageId = (int)reader["MessageId"];
            int senderUserId = (int)reader["SenderUserId"];
            int receiverUserId = (int)reader["ReceiverUserId"];
            string message = (string)reader["Message"];
            DateTime sendAt = (DateTime)reader["SendAt"];

            //return new User object
            return new Message(messageId, message, senderUserId, receiverUserId, sendAt);
        }

        public List<Message> GetMessages(int senderId, int receiverId)
        {
            List<Message> messages = new List<Message>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Messages WHERE (SenderUserId = @senderId AND ReceiverUserId = @receiverId) OR (SenderUserId = @receiverId AND ReceiverUserId = @senderId) ORDER BY SendAt ASC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@senderId", senderId);
                command.Parameters.AddWithValue("@receiverId", receiverId);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(ReadMessage(reader));
                }
            }
            return messages;
        }

        public void AddMessage(Message message)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Messages (SenderUserId, ReceiverUserId, Message, SendAt) VALUES (@senderUserId, @receiverUserId, @message, @sendAt)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@senderUserId", message.SenderUserId);
                command.Parameters.AddWithValue("@receiverUserId", message.ReceiverUserId);
                command.Parameters.AddWithValue("@message", message.MessageText);
                command.Parameters.AddWithValue("@sendAt", message.SendAt);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
