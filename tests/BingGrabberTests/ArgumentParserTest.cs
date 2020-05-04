using System;
using System.Linq;
using BingGrabber.Shared;
using NUnit.Framework;
using Shouldly;

namespace BingGrabberTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_create_dictionary()
        {
            string[] args = {"one=one","two=two"};

            var argumentParser = new BingGrabber.Shared.ArgumentParser(args);
            var result = argumentParser.Parse();

            result.Count.ShouldBe(2);
            result["one"].ShouldBe("one");
            result["two"].ShouldBe("two");
        }
        
        [Test]
        public void Invalid_arguments_throws_exception()
        {
            string[] args = {"one=one","two"};
            Should.Throw<ArgumentException>(() => new BingGrabber.Shared.ArgumentParser(args).Parse())
                .Message.ShouldBe("two");
        }

        public class CollectorSourceTests
        {
            [Test]
            public void Can_generate_datetime()
            {
                var args = new[] {"from=2019-01", "to=2020-01"};
                
                CollectorSource collectorSource = new CollectorSource(args);
                collectorSource.DateTimes.ToList().Count.ShouldBe(13);
            }
        }
    }
}