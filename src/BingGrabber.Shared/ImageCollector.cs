using System;
using System.IO;
using System.Threading.Tasks;
using BingGrabber.Shared.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Logging;

namespace BingGrabber.Shared
{
	public class ImageCollector : IImageCollector
	{
		private readonly ILogger<ImageCollector> _logger;
		private readonly IImageUriCollector _imageUriCollector;
		private readonly IArgumentParser _argumentParser;
		private string pathKey = "path";

		public ImageCollector(ILogger<ImageCollector> logger, IImageUriCollector imageUriCollector, IArgumentParser argumentParser)
		{
			_logger = logger;
			_imageUriCollector = imageUriCollector;
			_argumentParser = argumentParser;
		}

		public async Task Run()
		{
			if (!_argumentParser.ParsedValues.ContainsKey(pathKey))
			{
				_logger.LogError("Path not specified");
				throw new ArgumentException(pathKey);
			}

			if (!Directory.Exists(_argumentParser.ParsedValues[pathKey]))
			{
				_logger.LogInformation("Creating folder : {folder}", _argumentParser.ParsedValues[pathKey]);
				Directory.CreateDirectory(_argumentParser.ParsedValues[pathKey]);
			}

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
				var filename = Path.Combine(_argumentParser.ParsedValues["path"], $"{fragments[li]}");

				if (!File.Exists(filename))
				{
					_logger.LogInformation("Downloading image: {imageUrl}. Saving to {filename}", url, filename);
					var b = url.GetBytesAsync().GetAwaiter().GetResult();
					File.WriteAllBytes(filename, b);
				}
				else
				{
					_logger.LogDebug("Skipping existing image: {fileName}", filename);
				}
			});
		}
	}
}
