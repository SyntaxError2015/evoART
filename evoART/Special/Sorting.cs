using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evoART.Special
{
    public static class Sorting
    {
        public static int[] SortArrayDescending(int[] array, int numberToExtract)
        {
            var newArray = new int[numberToExtract];

            for (int i = 0, k = 0; i < array.Length && k < numberToExtract; i++, k++)
            {
                var max = array[i];
                var maxp = i;

                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[j] <= max) continue;
                    
                    max = array[j];
                    maxp = j;
                }

                array[maxp] = -1;
                newArray[k] = max;
            }

            return newArray;
        }
    }
}