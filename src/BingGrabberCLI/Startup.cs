using System.Threading.Tasks;
using BingGrabber.Shared;
using BingGrabber.Shared.Interfaces;

namespace BingGrabberCLI
{
	public class Startup
	{
		private readonly IImageCollector _imageCollector;

		public Startup(IImageCollector imageCollector)
		{
			_imageCollector = imageCollector;
		}

		public async Task Run() => await _imageCollector.Run();
	}
}
