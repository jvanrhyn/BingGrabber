using System;
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
    }
}