using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Scratchpad{

/*
* Problem:
* Given a set of sorted integers x and a value y - Find whether there exists a 
* subset of 2 integers in x that sum to y. 
*/
 public class TwoIntegerSum
 {

     // Brute Force solution: Time complexity = O(n^2) (n is length of array)
     public bool BruteForce(int[] sortedArray, int sum)
     {
         for(int i = 0; i < sortedArray.Length; i++)
            for(int j = i + 1; j < sortedArray.Length; j++)
            {
                if(sortedArray[i] + sortedArray[j] == sum)
                    return true;
            }

        return false;
     }

     // Better solution: Time Complexity = O(n*log n) (Because binary search is log n and in the worst case we do binary search n times)
     // Only works on a sorted array but you can sort in O(n*log n) time anyway, so it doesn't make a difference in terms of scaling
     public bool BinarySearchSolution(int[] sortedArray, int sum)
     {
         if(sortedArray.Length < 2)
            return false;

         for(int i = 0; i < sortedArray.Length; i++)
         {
             int complement = sum - sortedArray[i];

             if(BinarySearch(sortedArray,i + 1,sortedArray.Length - 1,complement) != -1)
                return true;
         }

         return false;
     }

     // Best solution (I think!): Time Complexity = O(n) because HashTable lookup is O(1)
     // Also works if the array isn't sorted
     public bool LookupSolution(int[] sortedArray, int sum)
     {
         HashSet<int> complementLookup = new HashSet<int>();
         bool result = false;
         for(int i = 0; i < sortedArray.Length; i++)
         {
             if(complementLookup.Contains(sortedArray[i]))
                return true;
             
             int complement = sum - sortedArray[i];
             if(!complementLookup.Contains(complement))
                complementLookup.Add(complement);
         }

         return result;
     }

     private int BinarySearch(int[] sortedArray, int start, int end, int valueToFind)
     {
        if (start <= end)
        {
            int middle = (start + end) / 2;
    
            if (sortedArray[middle] == valueToFind)
                return middle;
            else if (sortedArray[middle] > valueToFind)
                return BinarySearch(sortedArray,start, middle - 1, valueToFind);
            else if (sortedArray[middle] < valueToFind)
                return BinarySearch(sortedArray,middle + 1, end, valueToFind);
        }

        return -1;
     }
    
 }
 }