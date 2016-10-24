using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    async Task Go()
    {
        var hc = new HttpClient();
        var dic = new Dictionary<string, string>();
        dic["name"] = "masuda";
        dic["join"] = "1";
        dic["horoscope"] = "Taurus";
        dic["memo"] = "これはメモです";
        var cont = new FormUrlEncodedContent(dic);
        var url = "http://localhost/svQuest/post.php";
        var req = await hc.PostAsync(url, cont);
        var html = await req.Content.ReadAsStringAsync();
        Console.WriteLine(html);
    }
}
}
