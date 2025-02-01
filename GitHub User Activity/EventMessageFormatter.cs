namespace GitHub_User_Activity
{
    public static class EventMessageFormatter
    {

        private static readonly Dictionary<string, Func<EventMessageData, string>> Events = new()
        {
            {"PushEvent", e => FormatPushEvent("Pushed", e.RepoName, e.Commits)},
            {"PullRequestEvent", e => FormatEventWithAction(e.ActionName, "pull", e.RepoName)},
            {"IssuesEvent", e => FormatEventWithAction(e.ActionName, "issue", e.RepoName)},
            {"IssueCommentEvent", e => FormatEventWithAction(e.ActionName, "issue comment", e.RepoName)},
            {"ForkEvent", e => FormatDefaultEvent("Forked", e.RepoName)},
            {"WatchEvent", e => FormatDefaultEvent("Watched", e.RepoName)},
            {"CreateEvent", e => FormatDefaultEvent("Created", e.RepoName)},
            {"DeleteEvent", e => FormatDefaultEvent("Deleted", e.RepoName)},
            {"ReleaseEvent", e => FormatDefaultEvent("Released", e.RepoName)},
        };

        public static string GetMessageFormatted(EventMessageData eMessageData)
        {
            if (Events.TryGetValue(eMessageData.TypeEvent, out var formatter))
            {
                return formatter(eMessageData);
            }

            return "Event doesn't exist";
        }

        static public string FormatPushEvent(
            string typeEventName, 
            string repoName, 
            int commits
        ) =>
            $"{typeEventName} {commits} in {repoName}";
        

        static public string FormatEventWithAction(
            string actionName, 
            string typeActionName,
            string repoName
        )
        {
            var messageFragment = "";

            if (actionName.ToLower() == "created")
                messageFragment = "new ";

            actionName = Utils.CapitalizeFirstLetter(actionName);
            typeActionName = typeActionName.ToLower();

            return $"{actionName} a {messageFragment}{typeActionName} in {repoName}";
        }

        static public string FormatDefaultEvent(
            string typeEventName,
            string repoName
        ) =>
         $"{typeEventName} in {repoName}";



    }
}
