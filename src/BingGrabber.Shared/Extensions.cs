#nullable enable
using System;
using System.Collections.Generic;
using BingGrabber.Shared.Types;

namespace BingGrabber.Shared
{
	public static class Extensions
	{
		// https://codereview.stackexchange.com/a/196913
		public static IEnumerable<T> Interpolate<T>(this RangeData<T> rangeData, Func<T, T> next) where T : IComparable
		{
			var current = rangeData.Min;
			while (current.CompareTo(rangeData.Max) <= 0)
			{
				yield return (current = next(current));
			}
		}

	}
}
