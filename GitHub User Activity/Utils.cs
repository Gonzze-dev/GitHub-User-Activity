namespace GitHub_User_Activity
{
    public static class Utils
    {
        public static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }

    }
}
