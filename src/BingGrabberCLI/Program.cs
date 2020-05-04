using System;
using System.Threading.Tasks;
using BingGrabber.Shared;

namespace BingGrabberCLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Image collection");

            var imageCollector = new ImageCollector(args);
            await imageCollector.SaveImages();

            Console.WriteLine("Images collected");
            Console.ReadLine();
        }
    }
}
