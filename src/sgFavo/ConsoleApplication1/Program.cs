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
        // MACアドレスで暗号化するとか。
        const string ApiKey = "";
        const string ApiSecret = "";
        const string AccessToke = "";
        const string AccessTokeSecret = "";

        public async Task Go()
        {
            var tokens = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);
            var favs = await tokens.Favorites.ListAsync();

            foreach ( var it in favs )
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
