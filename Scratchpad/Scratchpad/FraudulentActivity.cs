using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Scratchpad
{
    /*
     * Solution to: https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem
     */
    public class FraudulentActivity
    {
        /*
         * Linear time O(n), passes all test cases without timing out 
         */
        public int Solve(int[] expenditureData, int slidingWindowLength)
        {
            if (slidingWindowLength < 1)
                throw new ArgumentOutOfRangeException(nameof(slidingWindowLength), "slidingWindow length must be greater than 0");
            
            if (slidingWindowLength > expenditureData.Length - 1)
                return 0;

            SpecializedQueue queue = new SpecializedQueue(slidingWindowLength, 200);

            int notifications = 0;

            // Pre fill our queue up to required size
            for (int j = 0; j < slidingWindowLength; j++)
            {
                queue.Enqueue(expenditureData[j]);
            }

            // Now start comparing each value against the median of the last sliding window
            for (int i = slidingWindowLength; i < expenditureData.Length; i++)
            {
                if (expenditureData[i] >= queue.GetMedian() * 2)
                    notifications++;

                // Now we have checked this element, put it in our queue and dequeue the last 
                // element, so we slideeeeee... :)
                queue.Enqueue(expenditureData[i]);
                queue.Dequeue();
            }

            return notifications;
        }

        /*
         * I don't know what to call this.
         * It uses an internal queue to preserve original order.
         * It also uses a concept from the counting sort algorithm to get
         * the median in linear time proportional to maxValue. Opposed
         * to maintining a seperate sorted priority queue which would cost time
         * proportional to the maxSize of the queue to maintain 
         * https://en.wikipedia.org/wiki/Counting_sort
         *
         */
        public class SpecializedQueue 
        {
            // Queue that preserves original order
            private Queue<int> _queue = new Queue<int>();
            // Keep a count of how many times a given element from 0-maxValue occurs in queue
            private int[] _occurrenceCount;
            // maximum allowed value of any given element
            private int _maxValue;

            public SpecializedQueue(int maxSize,int maxValue)
            {
                _queue = new Queue<int>();
                _occurrenceCount = new int[maxValue + 1];
                _maxValue = maxValue;
            }
          
            /*
             * Add value to queue and increment the occurrence count
             */
            public void Enqueue(int newValue)
            {
                if (newValue < 0 || newValue > _maxValue)
                    throw new ArgumentOutOfRangeException(nameof(newValue), $"This instance of the data structure only supports values between 0 and {_maxValue}!");

                _queue.Enqueue(newValue);
                _occurrenceCount[newValue]++;
            }

 
            public void Dequeue()
            {
                int valueRemoved = _queue.Dequeue();
                _occurrenceCount[valueRemoved]--;
            }

            public double GetMedian()
            {
                return _queue.Count % 2 != 0 ? GetMedianOdd() : GetMedianEven();
            }

            /*
             * Even tricker than Odd, there are a few gotchas
             */
            private double GetMedianEven()
            {
                int middle = _queue.Count / 2;
                double median = 0;
                int count = 0;
                for (int i = 0; i < _occurrenceCount.Length; i++)
                {
                    int nextCountValue = count + _occurrenceCount[i];
                    // Two middles must be distinct values
                    if (nextCountValue == middle)
                    {
                        // Find next middle element and then compute median
                        // Could perhaps make this a little neater but it will do
                        int j = i;
                        bool found = false;
                        while (!found)
                        {
                            j++; 
                            if (_occurrenceCount[j] > 0)
                                found = true;
                        }

                        median = (i + j) / 2.0;
                        break;
                    }

                    // Two middles must be the same value
                    if (nextCountValue > middle)
                    {
                        median = (i + i) / 2.0;
                        break;
                    }

                    count = nextCountValue;
                }

                return median;
            }

            /*
             * Odd case is easier than even just count up until we find it
             */
            private double GetMedianOdd()
            {
                int middle = _queue.Count / 2 + 1;
                int count = 0;
                int median = 0;
                for (int i = 0; i < _occurrenceCount.Length; i++)
                {
                    int nextCountValue = count + _occurrenceCount[i];

                    if (nextCountValue >= middle)
                    { 
                        median = i;
                        break;
                    }

                    count = nextCountValue;
                }

                return median;
            }
        }
    }

}

