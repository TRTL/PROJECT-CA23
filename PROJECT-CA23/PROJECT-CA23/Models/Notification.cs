namespace PROJECT_CA23.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int Title { get; set; }
        public int Text { get; set; }
        public bool Shown { get; set; }
    }
}
