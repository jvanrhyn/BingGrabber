#nullable enable
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using BingGrabber.Shared.Interfaces;
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

		public Dictionary<string, string> ParsedValues { get; } = new Dictionary<string, string>();

		private void Parse()
        {
            foreach (var item in _args)
            {
                var kv = item.Split('=');
                if (kv.Length != 2)
                {
	                _logger.LogError("Argument invalid {argument}", kv[0]);
                    throw new ArgumentException(kv[0]);
                }
                _logger.LogInformation("Adding argument {key}, {value}", kv[0], kv[1]);
                ParsedValues.Add(kv[0], kv[1]);
            }
            Validate();
        }

		private void Validate()
		{
			var mustHave = new[] {Constants.ToKey, Constants.FormKey, Constants.PathKey};
			if (ParsedValues.Keys.Intersect(mustHave).Count() != mustHave.Length)
			{
				var validationError =  new ArgumentException("Missing required parameter. from= to= and path= are required");
				_logger.LogError(validationError,"Validation Error");
				throw validationError;
			}
		}
    }
}
