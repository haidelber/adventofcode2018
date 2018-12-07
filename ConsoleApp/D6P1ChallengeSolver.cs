using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class D6P1ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0601.txt";

        public object SolveChallange()
        {
            var regex = new Regex("(?<X>\\d+), (?<Y>\\d+)");
            var parsedInput = File.ReadAllLines(InputPath).Select(s =>
            {
                var m = regex.Match(s);
                return new Tuple<int, int>(Convert.ToInt32(m.Groups["X"].Value), Convert.ToInt32(m.Groups["Y"].Value));
            }).ToList();
            var minX = parsedInput.Min(tuple => tuple.Item1);
            var maxX = parsedInput.Max(tuple => tuple.Item1);
            var minY = parsedInput.Min(tuple => tuple.Item2);
            var maxY = parsedInput.Max(tuple => tuple.Item2);

            var allDistances = new int[parsedInput.Count][][];
            for (var index = 0; index < parsedInput.Count; index++)
            {
                allDistances[index] = new int[maxX - minX][];
                var coords = parsedInput[index];
                for (int i = 0; i < maxX - minX; i++)
                {
                    allDistances[index][i] = new int[maxY - minY];
                    for (int j = 0; j < maxY - minY; j++)
                    {
                        allDistances[index][i][j] = Math.Abs(coords.Item1 - minX - i) + Math.Abs(coords.Item2 - minY - j);
                    }
                }
            }

            var grid = new int[maxX - minX,maxY - minY];
            var count = new int[parsedInput.Count];
            for (int i = 0; i < maxX - minX; i++)
            {
                for (int j = 0; j < maxY - minY; j++)
                {
                    var minDist = int.MaxValue;
                    var minIdx = 0;
                    for (var index = 0; index < parsedInput.Count; index++)
                    {
                        if (allDistances[index][i][j] < minDist)
                        {
                            minDist = allDistances[index][i][j];
                            minIdx = index;
                        }
                    }

                    grid[i, j] = minIdx;
                    count[minIdx]++;
                    Console.Write(grid[i, j]+" ");
                }
                Console.Write("\n");
            }



            Console.WriteLine(count);

            return null;
        }
    }
}