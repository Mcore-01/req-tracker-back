﻿
namespace req_tracker_back.ViewModels
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DisplayModel<int> Status { get; set; }
        public DisplayModel<int> Group { get; set; }
        public DisplayModel<string> Observer { get; set; }
        public DisplayModel<string>? Executor { get; set; }
        public string Text { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public bool IsLocked { get; set; }
    }
}