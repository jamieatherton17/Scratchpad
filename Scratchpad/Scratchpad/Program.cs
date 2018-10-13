using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] obs = new int[3][];
            obs[0] = new int[] {5, 5};
            obs[1] = new int[] {4, 2};
            obs[2] = new int[] {2, 3};


            int result = QueensAttack.queensAttack(5,3,4,3, obs);
        }
    }
}
