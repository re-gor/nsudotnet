using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    class Program
    {
        
        static void Main(string[] args)
        {
            WriteHelp();
            Console.ReadLine();
            Console.Clear();
            while (true)
            {
                Game game = new Game();
                game.Start();
                Console.WriteLine("Press any key");
                Console.ReadLine();
            }
            
        }
        static void WriteHelp()
        {
            StringBuilder sb = new StringBuilder("Welcome to an alternative tictactoe game", 200);
            sb.AppendLine("\nFor making turns use digits");
            string delimeter = "= = =";

            sb.AppendLine("1|2|3");
            sb.AppendLine(delimeter);
            sb.AppendLine("5|6|6");
            sb.AppendLine(delimeter);
            sb.AppendLine("7|8|9");
            sb.AppendLine("\nFor restart game press \"r\"");
            sb.AppendLine("Press any key to start game");

            Console.WriteLine(sb.ToString());
        }
    }
}
