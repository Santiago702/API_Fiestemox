using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Dropbox: I_Dropbox
    {
        private readonly IConfiguration _configuration;

        public S_Dropbox(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Token()
        {
            var clientId = _configuration["Dropbox:AppKey"];
            var clientSecret = _configuration["Dropbox:AppSecret"];
            var refreshToken = _configuration["Dropbox:RefreshToken"];

            using var httpClient = new HttpClient();

            var values = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync("https://api.dropboxapi.com/oauth2/token", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error refrescando token de Dropbox: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<DropboxToken>(
                json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return tokenResponse.AccessToken;
        }
    }
}
