using System.Threading.Tasks;
using BingGrabber.Shared;

namespace BingGrabberCLI
{
	public class BingLoader
	{
		private readonly IImageCollector _imageCollector;

		public BingLoader(IImageCollector imageCollector)
		{
			_imageCollector = imageCollector;
		}

		public async Task Run() => await _imageCollector.SaveImages();
	}
}
