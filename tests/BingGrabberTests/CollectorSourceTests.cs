using System.Linq;
using BingGrabber.Shared;
using NUnit.Framework;
using Shouldly;

namespace BingGrabberTests
{
	public class CollectorSourceTests
    {


	    [Test]
            public void Can_generate_datetime()
            {
                var args = new[] {"from=2019-01", "to=2020-01", "path=."};

				var argumentParser = new ArgumentParser(TestLogger.For<ArgumentParser>(), args);

                var collectorSource = new CollectorSource(TestLogger.For<CollectorSource>(), argumentParser );
                collectorSource.DateTimes.ToList().Count.ShouldBe(13);
            }
        }
    }

