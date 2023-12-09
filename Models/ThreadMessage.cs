namespace Forum.Models
{
    public class ThreadMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int ForumThreadId { get; set; }
        public DateTime Timestamp { get; set; }

        public ThreadMessage()
        {

        }
    }
}
