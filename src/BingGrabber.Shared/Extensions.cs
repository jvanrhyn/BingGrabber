using System;
using System.Collections.Generic;
using BingGrabber.Shared.Types;

namespace BingGrabber.Shared
{
	public static class Extensions
	{
		public static IEnumerable<T> Interpolate<T>(this Range<T> range, Func<T, T> next) where T : IComparable
		{
			var current = range.Min;
			while (current.CompareTo(range.Max) <= 0)
			{
				yield return (current = next(current));
			}
		}

	}
}
