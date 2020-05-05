using System;
using System.Globalization;
using BingGrabber.Shared.Types;

namespace BingGrabber.Shared
{
	// https://codereview.stackexchange.com/a/196913
	internal static class Range
	{
		public static RangeData<DateTime> FromDateTime(string min, string max, string format) =>
			new RangeData<DateTime>(
				DateTime.ParseExact(min, format, CultureInfo.InvariantCulture),
				DateTime.ParseExact(max, format, CultureInfo.InvariantCulture)
			);
	}
}
