using BingGrabber.Shared;
using NUnit.Framework;

namespace BingGrabberTests
{
	public partial class Tests
    {
		public class CollectorSourceTests
        {
			

			[Test]
            public void Can_generate_datetime()
            {
                var args = new[] {"from=2019-01", "to=2020-01", "path=."};

				var argumentParser = new ArgumentParser(TestLogger.Get<ArgumentParser>(), args);
                
                CollectorSource collectorSource = new CollectorSource(TestLogger.Get<CollectorSource>(), argumentParser );
                //collectorSource.DateTimes.ToList().Count.ShouldBe(13);
            }
        }
    }
}
