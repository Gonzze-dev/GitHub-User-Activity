using System.Net;
using System.Text.Json;

namespace GitHub_User_Activity
{
    public static class UserRepository
    {
        static readonly HttpClient _httpClient = new();
        
        static string Url (string username) => 
            $"https://api.github.com/users/{username}/events";

        public static async Task<JsonElement.ArrayEnumerator> GetUserActivity(string username)
        {
            var url = Url(username);
            
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    {"Accept", "application/vnd.github+json" },
                    {"User-Agent", "GitHub-User-Activity" }
                }
            };

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception($"Error, the username {username} no exist");
            else if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in the request: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var contentJson = JsonDocument.Parse(content).RootElement.EnumerateArray();

            return contentJson;
        }

    }
}
