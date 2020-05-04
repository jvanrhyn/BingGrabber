using System;
using System.Collections.Generic;

namespace BingGrabber.Shared
{
	public interface ICollectorSource
	{
		IEnumerable<DateTime> DateTimes { get; }
		IEnumerable<string> Urls { get; }
		void Build();
	}
}
