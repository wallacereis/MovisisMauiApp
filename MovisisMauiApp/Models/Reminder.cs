namespace MovisisMauiApp.Models
{
    [SQLite.Table("Reminder")]
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int ReminderId { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }

        // Relationships
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ReminderAttachment> ReminderAttachments { get; set; }
    }
}
