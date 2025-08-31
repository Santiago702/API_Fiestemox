using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Dropbox : I_Dropbox
    {
        private readonly IConfiguration _configuration;
        private string _tokenActual;
        private DateTime _expiracionToken;

        public S_Dropbox(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Token()
        {
            if (!string.IsNullOrEmpty(_tokenActual) && DateTime.UtcNow < _expiracionToken)
            {
                return _tokenActual;
            }

            var clientId = _configuration["Dropbox:AppKey"];
            var clientSecret = _configuration["Dropbox:AppSecret"];
            var refreshToken = _configuration["Dropbox:RefreshToken"];

            using var httpClient = new HttpClient();
            var values = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "refresh_token"),
            new("refresh_token", refreshToken),
            new("client_id", clientId),
            new("client_secret", clientSecret)
        };

            var content = new FormUrlEncodedContent(values);
            var response = await httpClient.PostAsync("https://api.dropboxapi.com/oauth2/token", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error refrescando token de Dropbox: {await response.Content.ReadAsStringAsync()}");

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<DropboxToken>(
                json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            _tokenActual = tokenResponse.AccessToken;
            _expiracionToken = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60); 
            return _tokenActual;
        }
    }

}
