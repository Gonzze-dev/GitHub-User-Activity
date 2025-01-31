namespace GitHub_User_Activity
{
    public static class ApiService
    {
    
        public static async Task GetUserActivity()
        {

            var contenJson = await UserRepository.GetUserActivity();
           
            foreach (var item in contenJson)
            {
                var payload = item.GetProperty("payload");
                var resultCommit = payload.TryGetProperty("commits", out var commits);

                var resultActionName = payload.TryGetProperty("action", out var actionName);
                
                EventMessageData eMessageData = new()
                {
                    TypeEvent = item.GetProperty("type").ToString(),
                    ActionName = resultActionName ? actionName.ToString() : "",
                    RepoName = item.GetProperty("repo").GetProperty("name").ToString(),
                    Commits = resultCommit ? commits.EnumerateArray().Count() : 0
                };

                var message = EventMessageFormatter.GetMessageFormatted(eMessageData);

                Console.WriteLine(message);
            }

            

        }
        
    }
}
