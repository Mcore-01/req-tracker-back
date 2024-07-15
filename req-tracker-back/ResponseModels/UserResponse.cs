using System.Text.Json.Serialization;

namespace req_tracker_back.ResponseModels
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        public string FullName => $"{LastName} {FirstName}";
    }
}