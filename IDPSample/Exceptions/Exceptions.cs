using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDPSample.Exceptions
{
    public class ConfigurationMissingException : Exception
    {
        public string ConfigurationKey { get; }

        public ConfigurationMissingException(string configurationKey)
            : base($"Configuration key '{configurationKey}' is missing.")
        {
            ConfigurationKey = configurationKey;
        }
    }
}