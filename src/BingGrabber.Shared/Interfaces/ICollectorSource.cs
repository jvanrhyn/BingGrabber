using System;
using System.Collections.Generic;

namespace BingGrabber.Shared.Interfaces
{
	public interface ICollectorSource
	{
		IEnumerable<DateTime> DateTimes { get; }
		List<string> Urls { get; }
		void Build();
	}
}
