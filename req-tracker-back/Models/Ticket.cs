namespace req_tracker_back.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public Status Status { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public string Observer { get; set; } = null!;
        public string? Executor { get; set; }
        public string? Text { get; set; }
        public string? Result { get; set; }
        public string? Comment { get; set; }
        public bool IsLocked { get; set; }
    }
}