using System.Text.Json.Serialization;

namespace req_tracker_back.ResponseModels
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}