namespace GitHub_User_Activity
{
    public class EventMessageData
    {
        public string TypeEvent { get; set; }
        public string ActionName { get; set; }
        public string RepoName { get; set; }
        public int Commits { get; set; }
    }
}
