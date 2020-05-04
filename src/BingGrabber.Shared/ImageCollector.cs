using System;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;

namespace BingGrabber.Shared
{
	public class ImageCollector
	{
		private readonly ImageUriCollector _imageUriCollector;
		public ImageCollector(string[] args)
		{
			_imageUriCollector = new ImageUriCollector(args);
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
					var b = url.GetBytesAsync().GetAwaiter().GetResult();
					File.WriteAllBytes(filename, b);
				}
			});
		}
	}
}
