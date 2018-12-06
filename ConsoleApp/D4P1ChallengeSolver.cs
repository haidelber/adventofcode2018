using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class D4P1ChallengeSolver : IChallengeSolver
    {
        public string InputPath { get; set; } = "0401.txt";
        public object SolveChallange()
        {
            var allInput = File.ReadAllLines(InputPath).ToImmutableSortedSet();
            var regexGuardBegin = new Regex("\\[(?<Timestamp>.*)\\] Guard #(?<Id>\\d+) begins shift");
            var regexWakesUp = new Regex("\\[(?<Timestamp>.*)\\] wakes up");
            var regexFallsAsleep = new Regex("\\[(?<Timestamp>.*)\\] falls asleep");
            // [1518-07-10 23:59] Guard #1901 begins shift
            // [1518-10-01 00:52] wakes up
            // [1518-08-01 00:49] falls asleep


            int guardId = 0;
            var fallAsleepTime = default(DateTime);
            var wakeUpTime = default(DateTime);

            var parsedSleep = new List<ShiftSleep>();

            foreach (var input in allInput)
            {
                var match = regexGuardBegin.Match(input);
                if (match.Success)
                {
                    guardId = Convert.ToInt32(match.Groups["Id"].Value);
                    continue;
                }

                match = regexFallsAsleep.Match(input);
                if (match.Success)
                {
                    fallAsleepTime = DateTime.ParseExact(match.Groups["Timestamp"].Value, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                    continue;
                }

                match = regexWakesUp.Match(input);
                if (match.Success)
                {
                    wakeUpTime = DateTime.ParseExact(match.Groups["Timestamp"].Value, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                    parsedSleep.Add(new ShiftSleep(guardId, fallAsleepTime, wakeUpTime));
                }
            }

            var guardSleepHistograms = new Dictionary<int, int[]>();

            var maxGuard = 0;
            var maxMin = 0;
            var maxSleep = 0;

            foreach (var shiftSleep in parsedSleep)
            {
                if (!guardSleepHistograms.ContainsKey(shiftSleep.GuardId))
                {
                    guardSleepHistograms.Add(shiftSleep.GuardId, new int[60]);
                }

                var histogram = guardSleepHistograms[shiftSleep.GuardId];
                for (int i = shiftSleep.FallAsleepTime.Minute; i < shiftSleep.WakeUpTime.Minute; i++)
                {
                    histogram[i]++;
                    }

                
            }

            foreach (var kvp in guardSleepHistograms)
            {
                var sum = kvp.Value.Sum();
                if (sum > maxSleep)
                {
                    maxSleep = sum;
                    maxGuard = kvp.Key;
                    var sleeping = 0;
                    for (var i = 0; i < kvp.Value.Length; i++)
                    {
                        if (kvp.Value[i] > sleeping)
                        {
                            sleeping = kvp.Value[i];
                            maxMin = i;
                        }
                    }
                }
            }

            return maxMin*maxGuard;
        }

        public struct ShiftSleep
        {
            public int GuardId { get; }
            public DateTime FallAsleepTime { get; }
            public DateTime WakeUpTime { get; }

            public ShiftSleep(int guardId, DateTime fallAsleepTime, DateTime wakeUpTime)
            {
                GuardId = guardId;
                FallAsleepTime = fallAsleepTime;
                WakeUpTime = wakeUpTime;
            }

            public override string ToString()
            {
                return $"#{GuardId}: {FallAsleepTime:HH:mm} - {WakeUpTime:HH:mm}";
            }
        }
    }
}
