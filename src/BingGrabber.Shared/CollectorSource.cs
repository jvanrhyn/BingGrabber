using System;
using System.Collections.Generic;
using System.Linq;
using BingGrabber.Shared.Exceptions;

namespace BingGrabber.Shared
{
	public class CollectorSource
	{
		private readonly ArgumentParser _argumentParser;

		public CollectorSource(string[] args)
		{
			_argumentParser = new ArgumentParser(args);
			Build();
		}

		public IEnumerable<DateTime> DateTimes { get; private set; }

		public IEnumerable<string> Urls { get; private set; }

		private void Build()
		{
			var arguments = _argumentParser.Parse();
			if (!arguments.ContainsKey("from") || !arguments.ContainsKey("to"))
			{
				throw new MissingArgumentException();
			}

			var range = Range.FromDateTime(arguments["from"],arguments["to"], "yyyy-MM");
			DateTimes = range.Interpolate(d => d.AddMonths(1));

			Urls = DateTimes.Select(x => $"https://bingwallpaper.anerg.com/us/{x.Year:D4}{x.Month:D2}");
		}
	}
}
