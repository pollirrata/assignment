using System;
using Gaona.Assigment.Web;
using Microsoft.Owin.Hosting;

namespace Gaona.Assignment
{
    /// <summary>
    /// This is just to start the application. I decided to use Owin to easily start the application for testing.
    /// This could also have the application deployed in a way that horizontal scaling is feasible and easily achieved.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string address = "http://localhost:9999/";

            using (WebApp.Start<Startup>(address))
            {
                Console.WriteLine("Server started @ localhost:9999");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
