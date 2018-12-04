using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class D3P1ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0301.txt";

        public object SolveChallange()
        {
            var claims = File.ReadAllLines(InputPath);
            //#1 @ 49,222: 19x20
            var regex = new Regex("#(?<ClaimId>\\d+) @ (?<X>\\d+),(?<Y>\\d+): (?<Width>\\d+)x(?<Height>\\d+)");
            var claimsToFullfill = new HashSet<ClaimedCoordinates>();
            var claimsDuplicated = new HashSet<ClaimedCoordinates>();
            foreach (var claim in claims)
            {
                var match = regex.Match(claim);
                var x = Convert.ToInt32(match.Groups["X"].Value);
                var y = Convert.ToInt32(match.Groups["Y"].Value);
                var width = Convert.ToInt32(match.Groups["Width"].Value);
                var height = Convert.ToInt32(match.Groups["Height"].Value);
                for (var i = x; i < x + width; i++)
                {
                    for (var j = y; j < y + height; j++)
                    {
                        var coords = new ClaimedCoordinates(i, j);
                        if (claimsToFullfill.Contains(coords))
                        {
                            claimsDuplicated.Add(coords);
                        }
                        else
                        {
                            claimsToFullfill.Add(coords);
                        }
                    }
                }
            }

            return claimsDuplicated.Count;
        }

        private struct ClaimedCoordinates
        {
            int X { get; }
            int Y { get; }

            public ClaimedCoordinates(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(ClaimedCoordinates other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is ClaimedCoordinates && Equals((ClaimedCoordinates)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }

            public static bool operator ==(ClaimedCoordinates left, ClaimedCoordinates right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(ClaimedCoordinates left, ClaimedCoordinates right)
            {
                return !left.Equals(right);
            }

            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }
    }
}