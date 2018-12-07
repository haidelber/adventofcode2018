using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class D5P2ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0501.txt";
        public object SolveChallange()
        {
            var allInput = File.ReadAllText(InputPath).Trim();
            var allChars = allInput.ToLowerInvariant().ToCharArray().Distinct().ToList();
            var expressions = BuildRegexFromChars(allChars).ToList();
            return DoReact(allInput, allChars, expressions).Min();
        }

        private IEnumerable<int> DoReact(string input, IEnumerable<char> chars, IList<Regex> expressions)
        {
            foreach (var c in chars)
            {
                var inputWithoutC = input.Replace(c.ToString(), "", StringComparison.InvariantCultureIgnoreCase);
                int length;
                do
                {
                    length = inputWithoutC.Length;
                    foreach (var regex in expressions)
                    {
                        inputWithoutC = regex.Replace(inputWithoutC, "");
                    }
                } while (length > inputWithoutC.Length);

                yield return inputWithoutC.Length;
            }
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
