
namespace req_tracker_back.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Status Status { get; set; } = null!;
        public User Observer { get; set; } = null!;
        public User Executor { get; set; } = null!;
        public string Text { get; set; } = null!;
        public bool IsLocked { get; set; }
    }
}