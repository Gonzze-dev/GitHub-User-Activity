namespace GitHub_User_Activity
{
    public static class EventMessageFormatter
    {

        private static readonly Dictionary<string, Func<EventData, string>> Events = new()
        {
            {"PushEvent", e => FormatEvent("Pushed", e.RepoName, e.Commits)},
            {"PullRequestEvent", e => FormatEvent(e.ActionName, "pull", e.RepoName)},
            {"IssuesEvent", e => FormatEvent(e.ActionName, "issue", e.RepoName)},
            {"IssueCommentEvent", e => FormatEvent(e.ActionName, "issue comment", e.RepoName)},
            {"ForkEvent", e => FormatEvent("Forked", e.RepoName)},
            {"WatchEvent", e => FormatEvent("Watched", e.RepoName)},
            {"CreateEvent", e => FormatEvent("Created", e.RepoName)},
            {"DeleteEvent", e => FormatEvent("Deleted", e.RepoName)},
            {"ReleaseEvent", e => FormatEvent("Released", e.RepoName)},
        };

        public static string GetMessageFormatted(EventData eMessageData)
        {
            bool existKey = Events.TryGetValue(eMessageData.TypeEvent, out var formatter);
            
            if (!existKey || formatter == null)
                throw new ArgumentException($"Event type '{eMessageData.TypeEvent}' is not supported.");

            return formatter(eMessageData);
        }

        static public string FormatEvent(
            string typeEventName, 
            string repoName, 
            int commits
        ) =>
            $"{typeEventName} {commits} in {repoName}";
        

        static public string FormatEvent(
            string actionName, 
            string typeActionName,
            string repoName
        )
        {
            var isCreated = actionName.Equals("created", 
                                        StringComparison.CurrentCultureIgnoreCase
                                        );
            
             var messageFragment = isCreated ? " a new " : " ";

            actionName = Utils.CapitalizeFirstLetter(actionName);
            typeActionName = typeActionName.ToLower();

            return $"{actionName}{messageFragment}{typeActionName} in {repoName}";
        }

        static public string FormatEvent(
            string typeEventName,
            string repoName
        ) 
        {
            var isCreated = typeEventName.Equals("created",
                                        StringComparison.CurrentCultureIgnoreCase
                                        );

            var messageFragment = isCreated ? "new respository" : "in";

         


            return $"{typeEventName} {messageFragment} {repoName}";
        }



    }
}
