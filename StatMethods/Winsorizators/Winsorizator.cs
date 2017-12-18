using System;
using System.Linq;

namespace StatMethods.Winsorizators
{
    public class Winsorizator
    {
        public double FindWinsorizedMedium(double[] inputArray, int winsorization)
        {
            Array.Sort(inputArray);
            //max and min of input aray
            double[] edges = new double[] { inputArray[0], inputArray[inputArray.Length - 1] };

            //skipping winsorization / 2 edges of the array, then adding min and max, then finding average 
            return inputArray.Skip(winsorization / 2).Take(inputArray.Length - winsorization)
                    .Concat(edges).Average();
        }
    }
}
