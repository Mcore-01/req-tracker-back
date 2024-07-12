using req_tracker_back.Models;
using req_tracker_back.Repositories;
using req_tracker_back.ViewModels;

namespace req_tracker_back.Services
{
    public class TicketsService(TicketsRepository repository)
    {
        private readonly TicketsRepository _repository = repository;

        public IEnumerable<TicketDTO> GetAll(string? filter)
        {
            return _repository.GetAll(filter).Select(GetTicketDTO);
        }

        public TicketDTO GetById(int id)
        {
            return GetTicketDTO(_repository.GetById(id));
        }

        public int Create(TicketDTO requestDTO)
        {
            var request = new Ticket()
            {
                Status = new() { Id = requestDTO.Status.Id },
                Observer = new() { Id = requestDTO.Observer.Id},
                Executor = new() { Id = requestDTO.Executor.Id },
                Text = requestDTO.Text,
                IsLocked = false,
            };
            return _repository.Create(request);
        }

        public void Update(TicketDTO requestDTO)
        {
            var request = new Ticket()
            {
                Id = requestDTO.Id,
                Status = new() { Id = requestDTO.Status.Id },
                Observer = new() { Id = requestDTO.Observer.Id },
                Executor = new() { Id = requestDTO.Executor.Id },
                Text = requestDTO.Text,
                IsLocked = false,
            };
            _repository.Update(request);
        }

        public void Delete(int ticketID)
        {
            _repository.Delete(ticketID);
        }

        private TicketDTO GetTicketDTO(Ticket ticket)
        {
            return new TicketDTO()
            {
                Id = ticket.Id,
                Status = new DisplayModel<int>() { Id = ticket.Status.Id, Name = ticket.Status.Name },
                Observer = new DisplayModel<int>() { Id = ticket.Observer.Id, Name = ticket.Observer.Nickname },
                Executor = new DisplayModel<int>() { Id = ticket.Executor.Id, Name = ticket.Executor.Nickname },
                Text = ticket.Text,
                IsLocked = ticket.IsLocked
            };
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _repository.GetAllStatuses();
        }
    }
}