using System;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Logging;

namespace BingGrabber.Shared
{
	public class ImageCollector : IImageCollector
	{
		private readonly ILogger<ImageCollector> _logger;
		private readonly IImageUriCollector _imageUriCollector;
		public ImageCollector(ILogger<ImageCollector> logger, IImageUriCollector imageUriCollector)
		{
			_logger = logger;
			_imageUriCollector = imageUriCollector;
		}

		public async Task SaveImages()
		{
			await _imageUriCollector.Collect();

			var parallelOptions = new ParallelOptions()
			{
				MaxDegreeOfParallelism = Environment.ProcessorCount
			};
			Parallel.ForEach(_imageUriCollector.ImageUrls, parallelOptions, item =>
			{
				var url = $"https:{item}";

				var fragments = item.Split('/');
				var li = fragments.Length - 1;
				var filename = $@"/Users/johan/Pictures/Bing/{fragments[li]}";

				if (!File.Exists(filename))
				{
					_logger.LogInformation("Downloading image: {imageUrl}", url);
					var b = url.GetBytesAsync().GetAwaiter().GetResult();
					File.WriteAllBytes(filename, b);
				}
				else
				{
					_logger.LogWarning("Skipping existing image: {fileName}", filename);
				}
			});
		}
	}
}
