namespace MovisisMauiApp.Models
{
    [SQLite.Table("ReminderAttachment")]
    public class ReminderAttachment
    {
        [PrimaryKey, AutoIncrement]
        public int ReminderAttachmentId { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Reminder))]
        public int ReminderId { get; set; }

        public string AttachmentFileName { get; set; }

        public DateTime CreationDate { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Reminder Reminder { get; set; }
    }
}
