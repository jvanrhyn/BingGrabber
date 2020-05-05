using System;
using System.Linq;
using BingGrabber.Shared;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BingGrabberTests
{
	public partial class Tests
    {
		[SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_create_dictionary()
        {
			string[] args = {"one=one","two=two", "path=path"};

            var argumentParser = new ArgumentParser(TestLogger.Get<ArgumentParser>(), args);
            var result = argumentParser.ParsedValues;

            result.Count.ShouldBe(3);
            result["one"].ShouldBe("one");
            result["two"].ShouldBe("two");
			result["path"].ShouldBe("path");
		}
        
        [Test]
        public void Invalid_arguments_throws_exception()
        {
            string[] args = {"one=one","two"};
            Should.Throw<ArgumentException>(() => new BingGrabber.Shared.ArgumentParser(TestLogger.Get<ArgumentParser>(), args))
                .Message.ShouldBe("two");
        }

		public static class TestLogger
		{
			public static NullLogger<T> Get<T>() => new NullLogger<T>();
		}
    }
}
