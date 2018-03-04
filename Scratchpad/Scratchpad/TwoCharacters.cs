using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
namespace Scratchpad{
public class TwoCharacters
{
  /*
  * Solution to problem: https://www.hackerrank.com/challenges/two-characters/problem (Passes all test cases)
  *
  * Overall time complexity: T(m*n) = O(n) where m is number of pairs and n is the length of the input string
  */
  public int Execute(string s) 
  {
        char[] distinctSymbols =  s.AsEnumerable().Distinct().ToArray();
        int longest = 0;

        foreach(var symbolPair in GetAllSymbolPairCombinations(distinctSymbols))
        {
            string stripped = RemoveAll(s,distinctSymbols.Where(x => x != symbolPair.symbol1 && x != symbolPair.symbol2).ToArray());
            if(IsAlternating(stripped)
                && stripped.Length > longest)
            {
                    longest = stripped.Length;
            }
            
        }

        return longest;
            
    }
    
    private string RemoveAll(string source, char[] toRemove)
    {
         return string.Join("", source.ToCharArray().Where(a => !toRemove.Contains(a)).ToArray());
    }
    private bool IsAlternating(string s)
    {
        int nextExpectedSymbolIndex = 0;
        char[] alternatingSymbols = new char[2];
        bool isAlternating = true;

        if(s.Length < 2 && s[0] != s[1]){
            isAlternating = false;
        }
        else{
            alternatingSymbols[0] = s[0];
            alternatingSymbols[1] = s[1];
            foreach(var symbol in s.AsEnumerable()){   
                if(symbol != alternatingSymbols[nextExpectedSymbolIndex]){
                    isAlternating = false;
                    break;
                }
                
                nextExpectedSymbolIndex = nextExpectedSymbolIndex == 1 ? 0 : 1;
            }
        }
        
        return isAlternating;
    }
    
    private IEnumerable<SymbolPair> GetAllSymbolPairCombinations(char[] symbols)
    {   
        int count = symbols.Count();
        for(int i = 0; i < count; i++)
            for(int j = i + 1; j < count; j++)
            {
                yield return new SymbolPair{
                    symbol1 = symbols[i],
                    symbol2 = symbols[j]
                };
            }
    }
  
    private struct SymbolPair{
        public char symbol1, symbol2;
    }
}
}