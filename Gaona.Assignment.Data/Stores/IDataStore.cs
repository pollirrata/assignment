namespace Gaona.Assignment.Data.Stores
{
    /// <summary>
    /// The contract defining the operations for the DataStore objects
    /// </summary>
    public interface IDataStore
    {
        void Add(string key, string value);
        string Get(string key);
    }
}
