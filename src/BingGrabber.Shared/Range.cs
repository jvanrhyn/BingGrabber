using System;
using System.Globalization;
using BingGrabber.Shared.Types;

namespace BingGrabber.Shared
{
	static class Range
	{
		public static Range<DateTime> FromDateTime(string min, string max, string format)
		{
			return new Range<DateTime>(
				DateTime.ParseExact(min, format, CultureInfo.InvariantCulture),
				DateTime.ParseExact(max, format, CultureInfo.InvariantCulture)
			);
		}
	}
}
