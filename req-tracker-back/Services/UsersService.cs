using req_tracker_back.Repositories;
using req_tracker_back.ResponseModels;
using req_tracker_back.ViewModels;

namespace req_tracker_back.Services
{
    public class UsersService(UsersRepository repository)
    {
        private readonly UsersRepository _repository = repository;

        public IEnumerable<ViewUserDTO> GetAll(string? filter)
        {
            return _repository.GetAll().Result.Select(GetViewUserDTO);
        }

        private ViewUserDTO GetViewUserDTO(UserResponse user) {
            return new ViewUserDTO
            {
                Id = user.Id,
                Name = user.FullName
            };
        }
    }
}