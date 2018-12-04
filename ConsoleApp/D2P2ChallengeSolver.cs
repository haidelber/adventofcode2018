using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public class D2P2ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0201.txt";

        public object SolveChallange()
        {
            var ids = File.ReadAllLines(InputPath);
            for (var i = 0; i < ids.Length; i++)
            {
                for (var j = i + 1; j < ids.Length; j++)
                {
                    int diffCount = 0;
                    for (int k = 0; k < ids[i].Length; k++)
                    {
                        if (ids[i][k] != ids[j][k])
                        {
                            diffCount++;
                            if (diffCount > 1)
                            {
                                break;
                            }
                        }
                    }

                    if (diffCount == 1)
                    {
                        return new string(CommonChars(ids[i], ids[j]).ToArray());
                    }
                }
            }

            throw new InvalidOperationException("Should find solution");
        }

        public IEnumerable<char> CommonChars(string str1, string str2)
        {
            for (var i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i])
                {
                    yield return str1[i];
                }
            }
        }
    }
}