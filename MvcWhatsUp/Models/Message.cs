namespace MvcWhatsUp.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public DateTime SendAt { get; set; }

        public Message()
        {
            MessageId = 0;
            MessageText = "";
            SenderUserId = 0;
            ReceiverUserId = 0;
            SendAt = DateTime.Now;
        }
        public Message(int messageId, string messageText, int senderId, int receiverId, DateTime sendAt)
        {
            MessageId = messageId;
            MessageText = messageText;
            SenderUserId = senderId;
            ReceiverUserId = receiverId;
            SendAt = sendAt;
        }
    }
}
