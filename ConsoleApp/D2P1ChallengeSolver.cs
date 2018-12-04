using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    public class D2P1ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0201.txt";
        public int CountWith2Duplicates { get; private set; } = 0;
        public int CountWith3Duplicates { get; private set; } = 0;

        public object SolveChallange()
        {
            var ids = File.ReadAllLines(InputPath);
            foreach (var id in ids)
            {
                SortedDictionary<char, int> histogram = new SortedDictionary<char, int>();
                foreach (var character in id)
                {
                    if (histogram.ContainsKey(character))
                    {
                        histogram[character]++;
                    }
                    else
                    {
                        histogram[character] = 1;
                    }
                }

                if (histogram.ContainsValue(2))
                {
                    CountWith2Duplicates++;
                }
                if (histogram.ContainsValue(3))
                {
                    CountWith3Duplicates++;
                }
            }

            return CountWith2Duplicates * CountWith3Duplicates;
        }
    }
}