using System.Collections;

namespace Forum.Models
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string userId { get; set; }
        public List<ThreadMessage> Messages { get; set; } = new List<ThreadMessage>();

        public ForumThread()
        {
            
        }
    }
}
