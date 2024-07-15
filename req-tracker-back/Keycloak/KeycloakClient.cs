using System.Net.Http.Headers;
using System.Text.Json;
using req_tracker_back.ResponseModels;

namespace req_tracker_back.Keycloak
{
    public class KeycloakClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public KeycloakClient() 
        {
            _httpClient = new HttpClient();
            _baseUrl = "http://localhost:8080";
        }

        public async Task<List<UserResponse>> GetUsersAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            var url = _baseUrl + "/admin/realms/rtrealm/users";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            var responseString = await response.Content.ReadAsStreamAsync();
            var users = await JsonSerializer.DeserializeAsync<List<UserResponse>>(responseString);

            return users;
        }

        public async Task<UserResponse> GetUserByIdAsync(string id)
        {
            var accessToken = await GetAccessTokenAsync();
            var url = _baseUrl + $"/admin/realms/rtrealm/users/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            var responseString = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserResponse>(responseString);

            return user;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            string url = _baseUrl + "/realms/rtrealm/protocol/openid-connect/token";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", "admin-cli"),
                new KeyValuePair<string, string>("client_secret", "3mWXgUmIYzyiSQUO9BaDU2qgDMZOu6vv"),
                new KeyValuePair<string, string>("username", "rtadmit"),
                new KeyValuePair<string, string>("password", "rtadmit"),
                new KeyValuePair<string, string>("grant_type", "password")
            });

            var response = await _httpClient.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStreamAsync();
            var tokenResponse = await JsonSerializer.DeserializeAsync<TokenResponse>(responseString);

            return tokenResponse.AccessToken;
        }
    }
}