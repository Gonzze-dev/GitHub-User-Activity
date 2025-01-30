using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHub_User_Activity
{
    public static class ApiService
    {
        
        static readonly HttpClient _httpClient = new();
        
        public static async Task GetUserActivity()
        {
            Dictionary<string, string> Events = new()
            {
                {"PushEvent", "Push" },
                {"PullRequestEvent",  "Pull"},
                {"IssuesEvent", "Issued"},
                {"IssueCommentEvent", "Issue comment"},
                {"ForkEvent", "Fork"},
                {"WatchEvent", "Watch"},
                {"CreateEvent", "Create"},
                {"DeleteEvent", "Delete"},
                {"ReleaseEvent", "Release"},
            };

            List<string> EventsWithActions = [
                "PullRequestEvent",
                "IssuesEvent",
                "IssueCommentEvent"
            ];

            var username = "kamranahmedse";
            var url = $"https://api.github.com/users/{username}/events";

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

            foreach ( var item in contentJson )
            {
                var type = item.GetProperty("type");
                var payload = item.GetProperty("payload");
                var repoName = item.GetProperty("repo").GetProperty("name");

                var typeEvent = type.ToString();
                var typeActionName = Events[typeEvent];

                if (typeEvent == "PushEvent")
                {
                    var countCommits = payload.GetProperty("commits").EnumerateArray().Count();

                    Console.WriteLine($"{typeActionName}ed {countCommits} in {repoName}");
                }
                else if (EventsWithActions.Contains(typeEvent))
                {
                    var action = payload.GetProperty("action").ToString();

                    action = Utils.CapitalizeFirstLetter(action);
                    typeActionName = typeActionName.ToLower();

                    Console.WriteLine($"{action} a new {typeActionName} in {repoName}");
                }
                else
                {
                    Console.WriteLine($"{typeActionName}ed in {repoName}");
                }
                
            }

            

        }
        
    }
}
