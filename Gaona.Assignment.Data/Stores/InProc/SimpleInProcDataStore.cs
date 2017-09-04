using System.Collections.Generic;

namespace Gaona.Assignment.Data.Stores.InProc
{
    /// <summary>
    /// Simple in-memory data store for testing purposes
    /// NOT FOR PRODUCTION USE
    /// </summary>
    public class SimpleInProcDataStore : IDataStore
    {
        private static Dictionary<string, string> _simpleDataStore;

        public SimpleInProcDataStore()
        {
            _simpleDataStore = new Dictionary<string, string>();

        }

        /// <summary>
        /// Adds the value to a strings dictionary, replacing the value if the key already exists
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <param name="value">The base64 string</param>
        public void Add(string key, string value)
        {
            if (_simpleDataStore.ContainsKey(key))
            {
                _simpleDataStore[key] = value;
            }
            else
            {
                _simpleDataStore.Add(key, value);
            }
        }

        /// <summary>
        /// Retrieves the data from the strings dictionary, if not exists then return null
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <returns>The base64 string</returns>
        public string Get(string key)
        {
            return _simpleDataStore.ContainsKey(key) ? _simpleDataStore[key] : null;
        }
    }
}
