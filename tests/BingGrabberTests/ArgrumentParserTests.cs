using System;
using BingGrabber.Shared;
using NUnit.Framework;
using Shouldly;

namespace BingGrabberTests
{
	public class ArgrumentParserTests
	{
		[Test]
		public void Can_create_dictionary()
		{
			string[] args = {"from=from", "to=to", "path=path"};

			var argumentParser = new ArgumentParser(TestLogger.For<ArgumentParser>(), args);
			var result = argumentParser.ParsedValues;

			result.Count.ShouldBe(3);
			result["from"].ShouldBe("from");
			result["to"].ShouldBe("to");
			result["path"].ShouldBe("path");
		}

		[Test]
		public void Invalid_arguments_throws_exception()
		{
			string[] args = {"one=one", "two"};
			Should.Throw<ArgumentException>(() =>
					new ArgumentParser(TestLogger.For<ArgumentParser>(), args))
				.Message.ShouldBe("two");
		}
	}
}

