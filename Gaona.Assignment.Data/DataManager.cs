using Gaona.Assignment.Data.Stores;
using Gaona.Assignment.Data.Stores.Azure;


namespace Gaona.Assignment.Data
{
    /// <summary>
    /// Class to manage the data operations. By default uses the BlobStorageDataStore,
    /// but if needed we can inject a different type based on IDataStore contract
    /// </summary>
    public class DataManager : IDataManager
    {
        private readonly IDataStore _dataStore;
        public DataManager()
        {
            _dataStore = new BlobStorageDataStore();
        }

        public DataManager(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Add to the specified data store
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <param name="value">Base64 encoded data</param>
        public void Add(string key, string value)
        {

            _dataStore.Add(key, value);

        }

        /// <summary>
        /// Checks if the item exists already, and if not returns the specified default value
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <param name="defaultValue">Data to return if the value is not found</param>
        /// <returns></returns>
        public string TryRetrieve(string key, string defaultValue = null)
        {
            string returnValue = _dataStore.Get(key);

            return string.IsNullOrEmpty(returnValue) ? defaultValue : returnValue;

        }
    }
}
