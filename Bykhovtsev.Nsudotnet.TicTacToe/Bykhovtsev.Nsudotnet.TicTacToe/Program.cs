using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToeConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Game.WriteHelp();
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Press any key");
            Game.PlayTicTacToe();
        }

        
    }
}
