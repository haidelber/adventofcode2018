using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(new D6P1ChallengeSolver().SolveChallange());
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
