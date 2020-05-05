using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
using Microsoft.Extensions.Logging;

[assembly:InternalsVisibleTo("BingGrabberTests")]
namespace BingGrabber.Shared
{
	public class ArgumentParser : IArgumentParser
	{
		private readonly ILogger<ArgumentParser> _logger;
		private readonly string[] _args;

        public ArgumentParser(ILogger<ArgumentParser> logger, string[] args)
        {
	        _logger = logger;
	        _args = args;
			Parse();
        }

		public Dictionary<string, string> ParsedValues { get; private set; }

		private void Parse()
        {
            var result = new Dictionary<string, string>();
            foreach (var item in _args)
            {
                var kv = item.Split('=');
                if (kv.Length != 2)
                {
	                _logger.LogError("Argument invalid {argument}", kv[0]);
                    throw new ArgumentException(kv[0]);
                }
                _logger.LogInformation("Adding argument {key}, {value}", kv[0], kv[1]);
                result.Add(kv[0], kv[1]);
            }

            ParsedValues = result;
        }
    }
}
