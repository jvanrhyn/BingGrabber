using System.Collections.Generic;
using System.Linq;
using Flurl.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BingGrabber.Shared
{
	public class ImageUriCollector
	{
		private readonly CollectorSource _collectorSource;
		public ImageUriCollector(string[] args)
		{
			_collectorSource = new CollectorSource(args);
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
