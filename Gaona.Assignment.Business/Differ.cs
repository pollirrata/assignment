using System;
using System.Collections.Generic;

namespace Gaona.Assignment.Business
{
    /// <summary>
    /// Concrete implementation for the differ
    /// </summary>
    public class Differ : IDiffer
    {
        public DiffResult Diff(string left, string right)
        {
            //Scenario 1: both values are the same
            if (left.Equals(right)) return new DiffResult("equal");



            //Scenario 2: Sizes are different
            byte[] leftBytes = Convert.FromBase64String(left);
            byte[] rightBytes = Convert.FromBase64String(right);

            if (leftBytes.LongLength != rightBytes.LongLength) return new DiffResult("different size");


            //Scenario 3: Same size, different content

            DiffResult result = new DiffResult("diffs") { Locations = new List<Location>() };

            Location location = null;
            int size = 0;

            for (long index = 0; index < leftBytes.LongLength; index++)
            {
                //check for the difference
                if (leftBytes[index] != rightBytes[index])
                {
                    size++;
                    //if null means we are not yet working on tracking a diff
                    if (location == null)
                    {
                        location = new Location() { Offset = index };
                    }
                }
                else
                {
                    if (location != null)
                    {
                        location.Size = size;
                        result.Locations.Add(location);
                        //we completed the process for the current diff,
                        //so we restart the size counter and reset to null the location
                        size = 0;
                        location = null;
                    }
                }

            }

            return result;
        }
    }
}