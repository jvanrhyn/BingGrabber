using System.Collections.Generic;

namespace BingGrabber.Shared
{
	public interface IArgumentParser
	{
		Dictionary<string, string> ParsedValues { get;  }
	}
}
