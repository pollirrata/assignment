namespace Gaona.Assignment.Business
{
    /// <summary>
    /// Class to hold the offset + size of the each difference
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Position of the difference
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Size of the difference
        /// </summary>
        public int Size { get; set; }
    }
}
