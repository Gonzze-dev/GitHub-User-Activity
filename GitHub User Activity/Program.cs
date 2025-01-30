
using GitHub_User_Activity;

try
{
    await ApiService.GetUserActivity();
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
