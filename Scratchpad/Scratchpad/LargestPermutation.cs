using System;
using System.Collections.Generic;
/*
* Solution for problem: https://www.hackerrank.com/challenges/largest-permutation/problem
*/
public class LargestPermutation{

    /*
    * k is max number of swaps and arr is the set of the first n natural numbers where n is array length
    * Find the largest lexicographic permutation of n with at most k swaps.
    * e.g arr=[4,2,3,5,1] with k = 1 then the largest permutation would be [5,2,3,4,1](swapping 4 and 5) 
    * which is the number 52,341
    */

    /*
    * Brute Force: Time Complexity = O(n^2) because in the worst-case we 
    * will perform the loop inside GetIndexOfLargestValue() n times
    */
    public int[] QuadraticTime(int k, int[] arr)
    {
        int numSwaps = 0;  
        for(int i = 0; i < arr.Length; i++)
        {
            int nextLargestIndex =  GetIndexOfLargestValue(arr,i);

            if(nextLargestIndex != i)
            {
                int temp = arr[i];
                arr[i] = arr[nextLargestIndex];
                arr[nextLargestIndex] = temp;
                numSwaps++;
            }

            if(numSwaps == k)
                break;
        }

        return arr;
    }
    
    /*
     * Linear time complexity: T(2n) = O(n) because in the worst-case
     * we loop once to create index map and then 
     * if the original array is in descending order and k = n
     * then we do n swaps, so we do a max of 2n constant time units
     * of work - Which is linear growth
     */
    public int[] LinearTime(int k, int[] arr)
    {
        Dictionary<int,int> indexMap = MapIndexes(arr);
        int numSwaps = 0;
        int largestValue = arr.Length;
        int i = 0;
        while(numSwaps < k && i < arr.Length)
        {
            int nextLargestValue = largestValue - i;     

            if(arr[i] != nextLargestValue)
            {
                int indexOfNextLargestValue = 0;

                if(!indexMap.TryGetValue(nextLargestValue, out indexOfNextLargestValue))
                    throw new Exception($"No key {nextLargestValue} found in dictionary");

                int temp = arr[i];
                arr[i] = arr[indexOfNextLargestValue];
                arr[indexOfNextLargestValue] = temp;

                indexMap[nextLargestValue] = i;
                indexMap[arr[indexOfNextLargestValue]] = indexOfNextLargestValue;
                numSwaps++;
            }

            i++;
        }

        return arr;
    }

    private Dictionary<int,int> MapIndexes(int[] arr)
    {
        Dictionary<int,int> dict = new Dictionary<int,int>();
        for(int i = 0; i < arr.Length; i++)
        {
            dict.Add(arr[i],i);
        }

        return dict;
    }

    private int GetIndexOfLargestValue(int[] arr, int startIndex)
    {
        int largest = 0;
        int largestIndex = 0;
        for(int i = startIndex; i < arr.Length; i++)
        {
            if(arr[i] > largest)
            {
                largest = arr[i];
                largestIndex = i;
            }
        }

        return largestIndex;
    }

}