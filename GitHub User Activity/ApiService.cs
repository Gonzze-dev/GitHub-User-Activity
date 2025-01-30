using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_User_Activity
{
    public static class ApiService
    {
        
        static readonly HttpClient httpclient = new();
    
        public static async void get()
        {
            var username = "Gonzze-dev";
            var url = $"https://api.github.com/users/{username}/events";

            var response = await httpclient.GetAsync(url);

            Console.WriteLine(response);
        }
    }
}
