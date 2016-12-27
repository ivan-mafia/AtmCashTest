using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmCashTest.ConsoleTestClient
{
    using AtmCashTest.ConsoleTestClient.AtmCashService;

    class Program
    {
        static void Main(string[] args)
        {
            GetResult();
            Console.ReadLine();
        }

        private async static void GetResult()
        {
            using (AtmCashServiceClient proxy = new AtmCashServiceClient())
            {
                var task = Task.Factory.StartNew(() => proxy.GetMessagesAsync("Hello"));
                var str = await task;
                await str.ContinueWith(
                    e =>
                        {
                            if (e.IsCompleted)
                            {
                                Console.WriteLine(str.Result);
                            }
                        });
                Console.WriteLine("Waiting for result");
            }
        }
    }
}
