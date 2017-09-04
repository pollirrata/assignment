namespace Gaona.Assignment.Data
{
    /// <summary>
    /// Contract defining the data manager structure. It is based on 2 simple operations,
    /// one for storing a string value associated to a key and the other to retrieve it.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Adds to the corresponding data store the value associated to a key
        /// </summary>
        /// <param name="key">key for the data item to store</param>
        /// <param name="value">value of the item to store</param>
        void Add(string key, string value);

        /// <summary>
        /// Get the item from the corresponding data store based on a key. 
        /// If not existing returns the default value.
        /// </summary>
        /// <param name="key">the item key</param>
        /// <param name="defaultValue">value to be returned if item not found, if not providede return null</param>
        /// <returns></returns>
        string TryRetrieve(string key, string defaultValue = null);
    }
}
