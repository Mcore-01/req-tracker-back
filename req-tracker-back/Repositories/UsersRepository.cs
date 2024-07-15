using req_tracker_back.Keycloak;
using req_tracker_back.ResponseModels;

namespace req_tracker_back.Repositories
{
    public class UsersRepository(KeycloakClient keycloak)
    {
        private readonly KeycloakClient _keycloak = keycloak;

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            return await _keycloak.GetUsersAsync();
        }

        public async Task<UserResponse> GetUserById(string id)
        {
            return await _keycloak.GetUserByIdAsync(id);
        }
    }
}