
namespace req_tracker_back.ViewModels
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public DisplayModel<int> Status { get; set; }
        public DisplayModel<int> Observer { get; set; }
        public DisplayModel<int> Executor { get; set; }
        public string Text { get; set; } = null!;
        public bool IsLocked { get; set; }
    }
}