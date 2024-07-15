using req_tracker_back.Models;
using req_tracker_back.Repositories;
using req_tracker_back.ViewModels;

namespace req_tracker_back.Services
{
    public class TicketsService(TicketsRepository repository, UsersRepository usersRepository)
    {
        private readonly TicketsRepository _repository = repository;
        private readonly UsersRepository _usersRepository = usersRepository;

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
                Observer = requestDTO.Observer.Id,
                Executor = requestDTO.Executor.Id,
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
                Observer = requestDTO.Observer.Id,
                Executor = requestDTO.Executor.Id,
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
            var observer = _usersRepository.GetUserById(ticket.Observer).Result;
            var executor = _usersRepository.GetUserById(ticket.Executor).Result;

            return new TicketDTO()
            {
                Id = ticket.Id,
                Status = new DisplayModel<int>() { Id = ticket.Status.Id, Name = ticket.Status.Name },
                Observer = new DisplayModel<string>() { Id = observer.Id, Name = observer.FullName },
                Executor = new DisplayModel<string>() { Id = executor.Id, Name = executor.FullName },
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