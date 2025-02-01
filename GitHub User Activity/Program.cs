using GitHub_User_Activity;

try
{
    Console.Write("github-activity: ");
    var username = Console.ReadLine();

    if (username == null || username.Length == 0)
        throw new Exception("Error, you must enter a username");

    var result = await ApiService.GetUserActivity(username);

    Console.WriteLine();
    ApiService.ShowActivity(result);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
