#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using BingGrabber.Shared.Exceptions;
using BingGrabber.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace BingGrabber.Shared
{
	public class CollectorSource : ICollectorSource
	{
		private readonly ILogger<CollectorSource> _logger;
		private readonly IArgumentParser _argumentParser;

		public CollectorSource(ILogger<CollectorSource> logger, IArgumentParser argumentParser)
		{
			_logger = logger;
			_argumentParser = argumentParser;
			Build();
		}

		public IEnumerable<DateTime> DateTimes { get; private set; } = Enumerable.Empty<DateTime>();

		public List<string> Urls { get; private set; } = new List<string>();

		public void Build()
		{
			_logger.LogInformation("Collecting sources.");
			var arguments = _argumentParser.ParsedValues;
			if (!arguments.ContainsKey("from") || !arguments.ContainsKey("to"))
			{
				_logger.LogError("Passed in parameters are invalid");
				throw new MissingArgumentException();
			}

			var range = Range.FromDateTime(arguments["from"],arguments["to"], "yyyy-MM");
			DateTimes = range.Interpolate(d => d.AddMonths(1));

			Urls = DateTimes.Select(x => $"https://bingwallpaper.anerg.com/us/{x.Year:D4}{x.Month:D2}").ToList();
			_logger.LogInformation("Found {count} pages with images", Urls.Count);
		}
	}
}
