using System.Collections.Generic;

namespace Gaona.Assignment.Business
{
    /// <summary>
    /// Holds the result of the diff process and the actual differences if any
    /// </summary>
    public class DiffResult
    {
        public DiffResult(string description)
        {
            Description = description;
        }
        /// <summary>
        /// Description of the result, either equal, different size or diffs
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Offset and sizes of the differences
        /// </summary>
        public IList<Location> Locations { get; set; }
    }
}
