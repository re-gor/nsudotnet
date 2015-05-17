using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalGame = Bykhovtsev.Nsudotnet.TicTacToe;

namespace Bykhovtsev.Nsudotnet.TicTacToeConsole
{
    static class Game
    {
        public static void WriteHelp()
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

        public static void PlayTicTacToe()
        {
            InternalGame.Game game = new InternalGame.Game();
            while (!game.IsGameStopped)
            {
                Console.Clear();
                PaintField(game);
                if (game.MandatoryField == 0)
                {
                    Console.WriteLine("{0}-player can choose any field, please choose field and cell: ", game.CurrentPlayer);
                }
                else
                {
                    Console.WriteLine("{0}-player should put his {0} in field {1}, please choose cell: ", game.CurrentPlayer, game.MandatoryField);
                }
                string userInput = Console.ReadLine();
                switch (game.MakeTurn(userInput))
                {
                    case InternalGame.GameResponseCode.GameEnded:
                    case InternalGame.GameResponseCode.TurnCompleted:
                        break;
                    case InternalGame.GameResponseCode.RepeatTurnBadCellNumber:
                        RepeatTurn("Wrong cell coordinate.");
                        break;
                    case InternalGame.GameResponseCode.RepeatTurnBadFieldNumber:
                        RepeatTurn("Wrong field coordinate.");
                        break;
                    case InternalGame.GameResponseCode.RepeatTurnBadInput:
                        RepeatTurn("What do you mean? Wrong coordinates.");
                        break;
                    case InternalGame.GameResponseCode.RepeatTurnFullField:
                        RepeatTurn("This field is full. Please choose another one.");
                        break;
                    case InternalGame.GameResponseCode.RepeatTurnNotEmptyCell:
                        RepeatTurn("This cell is not empty. Please choose another one.");
                        break;
                    default:
                        throw new Exception("WTF? Unknown response. Deep Blue, is that you?");
                }
            }

            if (game.IsDraw)
            {
                Console.WriteLine("Nobody wins... Draw.");
            }
            else if (game.Winner != InternalGame.Symbol.Empty)
            {
                Console.WriteLine("Winner is {0}! Congratulations!", game.Winner);
            }
            else
            {
                Console.WriteLine("Game restarted");
            }
        }

        private static string SymbolToString(InternalGame.Symbol symbol)
        {
            switch (symbol)
            {
                case InternalGame.Symbol.Cross:
                    return "X";
                case InternalGame.Symbol.Nought:
                    return "O";
                case InternalGame.Symbol.Empty:
                    return " ";
            }
            return "";
        }

        private static void PaintField(InternalGame.Game game)
        {
            InternalGame.Symbol[,] field = game.GetGameField();
            InternalGame.Symbol[,] generalView = game.GetGameGeneralView();

            string delimeter = " ===== || ===== || ===== ";
            Console.WriteLine("====Detailed View====\n");
            for (byte i = 0; i < 9; ++i)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine(delimeter);
                }
                for (byte j = 0; j < 9; ++j)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" ||");
                    }
                    Console.Write(" {0}", SymbolToString(field[i, j]));
                }
                Console.WriteLine();
                
                
            }
            Console.WriteLine("\n===Global View===");
            delimeter = "\n- - -";
            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    Console.Write(SymbolToString(generalView[i, j]));
                    if (j < 2)
                        Console.Write("|");
                }
                if (i < 2)
                {
                    Console.WriteLine(delimeter);
                }

            }
            Console.Write("\n");
        }

        private static void RepeatTurn(string reasonMessage)
        {
            Console.WriteLine("{0} Press any key to repeat turn", reasonMessage);
            Console.ReadLine();
        }

      
    }
}
