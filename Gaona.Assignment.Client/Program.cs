using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Gaona.Assignment.Client
{
    /// <summary>
    /// This application is intended for integration testing. It runs 
    /// different test cases, encoding the files added to the project.
    /// </summary>
    class Program
    {
        private static string _endpoint = ConfigurationManager.AppSettings["server"] ?? "http://localhost:9999/";
        static void Main(string[] args)
        {
            int sequence = 0;

            //Test #1 Payload too large
            string id = $"id-{++sequence}";
            Console.WriteLine("This file should return HTTP 413");
            PostFile("big-01.jpg", id, "left");

            ShowContinueMessage();

            //Test#2 Equal
            id = $"id-{++sequence}";
            Console.WriteLine("This pair of files should return 'equal'");
            PostFile("small-01.jpg", id, "left");
            PostFile("small-01.jpg", id, "right");
            GetDiff(id);

            ShowContinueMessage();

            //Test#3 Different size
            id = $"id-{++sequence}";
            Console.WriteLine("This pair of files should return 'different size'");
            PostFile("small-01.jpg", id, "left");
            PostFile("small-03.jpg", id, "right");
            GetDiff(id);

            ShowContinueMessage();


            id = $"id-{++sequence}";
            Console.WriteLine("This test should return 'different' and the offsets + sizes");
            PostFile("lipsum01.txt", id, "left");
            PostFile("lipsum02.txt", id, "right");
            GetDiff(id);

            ShowContinueMessage();


            id = $"id-{++sequence}";
            Console.WriteLine("This test ALSO should return 'different' and the offsets + sizes");
            PostFile("big-03-tampered.png", id, "left");
            PostFile("big-03.png", id, "right");
            GetDiff(id);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();


        }

        public static void PostFile(string filename, string id, string side)
        {

            byte[] file01Bytes = File.ReadAllBytes(filename);

            string base64 = Convert.ToBase64String(file01Bytes);


            HttpClient client = new HttpClient();


            var response = client.PostAsync($"{_endpoint}v1/diff/{id}/{side}", new StringContent($"{{'data': '{base64}'}}", Encoding.UTF8, "application/json")).Result;
            Console.WriteLine($"{(int)response.StatusCode} - {response.ReasonPhrase}");

        }

        public static void GetDiff(string id)
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync($"{_endpoint}v1/diff/{id}").Result;
            Console.WriteLine($"{(int)response.StatusCode}\n{response.Content.ReadAsStringAsync().Result}");

        }

        public static void ShowContinueMessage()
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press any key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
