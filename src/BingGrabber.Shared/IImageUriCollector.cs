using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingGrabber.Shared
{
	public interface IImageUriCollector
	{
		List<string> ImageUrls { get; }
		Task Collect();
	}
}
