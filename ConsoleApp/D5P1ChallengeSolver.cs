using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class D5P1ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0501.txt";

        public object SolveChallange()
        {
            var allInput = File.ReadAllText(InputPath).Trim();
            var expressions = BuildRegexFromChars(allInput.ToCharArray().Distinct().Select(char.ToLower).Distinct())
                .ToList();
            int length;
            do
            {
                length = allInput.Length;
                foreach (var regex in expressions)
                {
                    allInput = regex.Replace(allInput, "");
                }
            } while (length > allInput.Length);

            return allInput.Length;
        }

        private IEnumerable<Regex> BuildRegexFromChars(IEnumerable<char> chars)
        {
            StringBuilder sb = new StringBuilder("");
            foreach (var c in chars)
            {
                yield return new Regex($"{Char.ToLower(c)}{Char.ToUpper(c)}");
                yield return new Regex($"{Char.ToUpper(c)}{Char.ToLower(c)}");
            }
        }
    }
}


