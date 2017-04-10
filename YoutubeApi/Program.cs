using System;
using System.Threading.Tasks;

namespace YoutubeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");
            try
            {
                var youtubeServices = new YouTubeServices();
                var parameter=Console.ReadLine();
                var videos= await youtubeServices.GetVideos(parameter);

                foreach (var video in videos)
                {
                    Console.WriteLine("Title: "+video.Title);
                    Console.WriteLine("Description: "+video.Description);
                    Console.WriteLine("Url: "+video.Url);
                    Console.WriteLine("--------------------------------------------");
                }

            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}