using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingGrabber.Shared.Interfaces
{
	public interface IImageUriCollector
	{
		List<string> ImageUrls { get; }
		Task Collect();
	}
}
