using Microsoft.Extensions.Logging.Abstractions;

namespace BingGrabberTests
{
	internal static class TestLogger
	{
		public static NullLogger<T> For<T>() => new NullLogger<T>();
	}
}