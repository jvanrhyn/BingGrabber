#nullable enable
using System.Collections.Generic;
using System.Linq;
using Flurl.Http;
using System.Threading.Tasks;
using BingGrabber.Shared.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace BingGrabber.Shared
{
	public class ImageUriCollector : IImageUriCollector
	{
		private readonly ILogger<ImageUriCollector> _logger;
		private readonly ICollectorSource _collectorSource;
		public ImageUriCollector(ILogger<ImageUriCollector> logger, ICollectorSource collectorSource)
		{
			_logger = logger;
			_collectorSource = collectorSource;
		}

		public List<string> ImageUrls { get; } = new List<string>();

		public async Task Collect()
		{
			foreach (var url in _collectorSource.Urls)
			{
				var html = await url.GetStringAsync();
				var doc = new HtmlDocument();
				doc.LoadHtml(html);
				ImageUrls.AddRange(doc.DocumentNode
					.SelectNodes("//img")
					.Select(img => img.GetAttributeValue("src", string.Empty))
					.Where(src => src.StartsWith("//")));
			}
		}
	}
}
