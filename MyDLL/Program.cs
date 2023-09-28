using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace MyDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.GetDirectories("", "*");
            Get();
            Console.ReadKey();
        }
        public static async void Get()
        {
            for (int i = 0; i < 10000; i++)
            {
                var httpclient = new HttpClient();
                var reson = await httpclient.GetAsync("http://xh.bozaiq.cn:8084/api/Post/GetAll");
                Task.Delay(500).Wait();
                Console.WriteLine( await reson.Content.ReadAsStringAsync());
            }
        }
    }
}
