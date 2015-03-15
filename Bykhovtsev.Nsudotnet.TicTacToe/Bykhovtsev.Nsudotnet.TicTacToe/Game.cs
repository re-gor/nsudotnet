using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    class Game
    {
        private Field _gameField = new Field();
        private Symbol _currPlayer = Symbol.Cross;
        private byte _mandatoryField = 0;

        private void ChangePlayer ()
        {
            if (_currPlayer == Symbol.Cross)
                _currPlayer = Symbol.Nought;
            else
                _currPlayer = Symbol.Cross;
        }

        private void RepeatTurn (string reasonMessage)
        {
            Console.WriteLine("{0} Press any key to repeat turn", reasonMessage);
            Console.ReadLine();
        }

        private bool MakeFreeTurn()
        {
            Console.WriteLine("{0}-player can choose any field, please choose field and cell: ", _currPlayer);
            string digits = Console.ReadLine();
            if (digits == "r")
                return false;
            byte number = 0;
            if (byte.TryParse(digits, out number))
            {
                byte subFieldNumber = (byte)(number / 10);
                byte cellNumber = (byte)(number % 10);

                if (subFieldNumber > 9 || subFieldNumber == 0)
                {
                    RepeatTurn("Wrong field coordinate.");
                    return true;
                }
                if (cellNumber == 0)
                {
                    RepeatTurn("Wrong cell coordinate.");
                    return true;
                }

                subFieldNumber--;
                cellNumber--;

                if (_gameField.IsSubFieldFull(subFieldNumber))
                {
                    RepeatTurn("This field is full. Please choose another one.");
                    return true;
                }

                if (_gameField.TryPutValue(subFieldNumber, cellNumber, _currPlayer))
                {
                    if (_gameField.IsSubFieldFull(cellNumber))
                    {
                        _mandatoryField = 0;
                    }
                    else
                    {
                        _mandatoryField = ++cellNumber;
                    }

                    ChangePlayer();
                    return true;
                }
                else
                {
                    RepeatTurn("This cell is not empty. Please choose another one.");
                    return true;
                }

            }
            else
            {
                RepeatTurn("What do you mean? Wrong coordinates.");
                return true;
            }
        }

        private bool MakeMandatoryTurn()
        {
            Console.WriteLine("{0}-player should put his {0} in field {1}, please choose cell: ", _currPlayer, _mandatoryField);
            string digit = Console.ReadLine();
            if (digit == "r")
                return false;
            byte cellNumber = 0;
            if (byte.TryParse(digit, out cellNumber))
            {

                if (cellNumber > 9 || cellNumber == 0)
                {
                    RepeatTurn("Wrong cell coordinate.");
                    return true; 
                }

                cellNumber--;
                byte subField = (byte)(_mandatoryField - 1);
                if (_gameField.TryPutValue(subField, cellNumber, _currPlayer))
                {
                    if (_gameField.IsSubFieldFull(cellNumber))
                    {
                        _mandatoryField = 0;
                    }
                    else
                    {
                        _mandatoryField = ++cellNumber;
                    }
                    ChangePlayer();
                    return true;
                }
                else
                {
                    RepeatTurn("This cell is not empty. Please choose another one.");
                    return true;
                }

            }
            else
            {
                RepeatTurn("What do you mean? Wrong coordinates.");
                return true;
            }
        }

        private void End()
        {
            Console.Clear();
            _gameField.Paint();
            if (_gameField.IsDraw)
                Console.WriteLine("Nobody wins... Draw.");
            else if (_gameField.Winner != Symbol.Empty)
                Console.WriteLine("Winner is {0}! Congratulations!", _gameField.Winner);
            else
                Console.WriteLine("Game restarted");
        }

        public void Start ()
        {
            while (_gameField.Winner == Symbol.Empty && !_gameField.IsDraw)
            {
                Console.Clear();
                _gameField.Paint();

                bool isContinue;

                if (_mandatoryField == 0)
                {
                    isContinue = MakeFreeTurn();
                    
                }
                else
                {
                    isContinue = MakeMandatoryTurn();
                }

                if (isContinue)
                    continue;
                else
                    break;

            }
            End();
            
        }

        

    }
}
