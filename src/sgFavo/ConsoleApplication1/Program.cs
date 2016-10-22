using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            var t = prog.Go();
            t.Wait();

        }

        // ここは消しておくこと!!! 
        const string ApiKey = "API_KEY";
        const string ApiSecret = "API_SECRET";
        const string AccessToken = "ACCESS_TOKEN";
        const string AccessTokenSecret = "ACCESS_TOKEN_SECRET";

        public async Task Go()
        {
            var tokens = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToken, AccessTokenSecret);
            var favs = await tokens.Favorites.ListAsync();

            foreach (var it in favs)
            {
                Console.WriteLine("---");
                Console.WriteLine($"Id: {it.Id}");
                Console.WriteLine($"Name: {it.User.ScreenName} {it.User.Name}");
                Console.WriteLine(it.Text);
            }
            return;
        }
    }
}
