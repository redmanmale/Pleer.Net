using System;
using System.Collections.Generic;

namespace PleerNet
{
    internal class RequestParameters : Dictionary<string, string>
    {
        public void Add(string key, Enum value) => Add(key, value.ToString("G").ToLowerInvariant());

        public void Add(string key, int value) => Add(key, value.ToString());
    }
}
