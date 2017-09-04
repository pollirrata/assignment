namespace Gaona.Assignment.Business
{
    /// <summary>
    /// Contract to define the diff-er method. This helps we can have no hard dependendy 
    /// with the actual implementation of the logic and also mock it for unit testing the controllers
    /// </summary>
    public interface IDiffer
    {
        /// <summary>
        /// method to perform the actual diff 
        /// </summary>
        /// <param name="left">base-64 JSON string</param>
        /// <param name="right">base-64 JSON string</param>
        /// <returns>DiffResult instance with the description and list of diffs</returns>
        DiffResult Diff(string left, string right);
    }
}
