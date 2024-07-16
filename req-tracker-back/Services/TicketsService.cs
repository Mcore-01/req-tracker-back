using req_tracker_back.Models;
using req_tracker_back.Repositories;
using req_tracker_back.ResponseModels;
using req_tracker_back.ViewModels;

namespace req_tracker_back.Services
{
    public class TicketsService(TicketsRepository repository, UsersRepository usersRepository)
    {
        private readonly TicketsRepository _repository = repository;
        private readonly UsersRepository _usersRepository = usersRepository;

        public IEnumerable<TicketDTO> GetAll(string? filter)
        {
            var users = _usersRepository.GetAll().Result;
            return _repository.GetAll(filter).Select(ticket =>
            {
                var observer = users.First(p => p.Id == ticket.Observer);
                var executor = users.FirstOrDefault(p => p.Id == ticket.Executor);
                var ticketDTO = GetTicketDTO(ticket, observer, executor);
                return ticketDTO;
            });
        }

        public TicketDTO GetById(int id)
        {
            var ticket = _repository.GetById(id);
            var observer = _usersRepository.GetUserById(ticket.Observer).Result;

            UserResponse? executor = null;
            if (ticket.Executor is not null)
            {
                executor = _usersRepository.GetUserById(ticket.Executor).Result;
            }

            var ticketDTO = GetTicketDTO(ticket, observer, executor);
            return ticketDTO;
        }

        public int Create(string observerID)
        {
            var status = _repository.GetCreateStatus();
            var group = _repository.GetCreateGroup();
            var number = $"REQ-{Guid.NewGuid().ToString()}";
            var request = new Ticket()
            {
                Status = status,
                Number = number,
                Group = group,
                Observer = observerID,
                IsLocked = false,
            };
            return _repository.Create(request);
        }

        public void Update(TicketDTO requestDTO)
        {
            var request = new Ticket()
            {
                Id = requestDTO.Id,
                Number = requestDTO.Number,
                Status = new() { Id = requestDTO.Status.Id },
                Group = new() { Id = requestDTO.Group.Id },
                Observer = requestDTO.Observer.Id,
                Executor = requestDTO.Executor.Id,
                Text = requestDTO.Text,
                Result = requestDTO.Result,
                Comment = requestDTO.Comment,
                IsLocked = false,
            };
            _repository.Update(request);
        }

        public void Delete(int ticketID)
        {
            _repository.Delete(ticketID);
        }

        private TicketDTO GetTicketDTO(Ticket ticket, UserResponse observer, UserResponse? executor)
        {
            DisplayModel<string>? executorDTO = null;

            if (ticket.Executor is not null)
            {
                executorDTO = new DisplayModel<string>() { Id = executor.Id, Name = executor.FullName };
            }

            return new TicketDTO()
            {
                Id = ticket.Id,
                Number = ticket.Number,
                Status = new DisplayModel<int>() { Id = ticket.Status.Id, Name = ticket.Status.Name },
                Group = new DisplayModel<int>() { Id = ticket.Group.Id, Name = ticket.Group.Name },
                Observer = new DisplayModel<string>() { Id = observer.Id, Name = observer.FullName },
                Executor = executorDTO,
                Text = ticket.Text,
                Result = ticket.Result,
                Comment = ticket.Comment,
                IsLocked = ticket.IsLocked
            };
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _repository.GetAllStatuses();
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _repository.GetAllGroups();
        }
    }
}