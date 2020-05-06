using System.Collections.Generic;

namespace BingGrabber.Shared.Interfaces
{
	public interface IArgumentParser
	{
		Dictionary<string, string> ParsedValues { get; }
	}
}
