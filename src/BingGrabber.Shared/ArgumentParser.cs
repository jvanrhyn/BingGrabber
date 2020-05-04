using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System;

[assembly:InternalsVisibleTo("BingGrabberTests")]
namespace BingGrabber.Shared
{
	public class ArgumentParser : IArgumentParser
	{
        private readonly string[] _args;

        public ArgumentParser(string[] args)
        {
            _args = args;
        }

        public Dictionary<string, string> Parse()
        {
            var result = new Dictionary<string, string>();
            foreach (var item in _args)
            {
                var kv = item.Split('=');
                if (kv.Length != 2)
                {
                    throw new ArgumentException(kv[0]);
                }
                result.Add(kv[0], kv[1]);
            }

            return result;
        }
    }
}
