using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    /// <summary>
    /// <para>Free turn - when player can choose field and cell in this field</para>
    /// <para>Mandatory turn - when player should make turn in particular field (after previous player's turn)</para>
    /// <para>Game will be stoped in cases: <list type="bullet">
    /// <item><term>Draw</term><description>There is no winner in current game</description></item>
    /// <item><term>Winner</term><description>There is particular winner in current game: cross or nougth</description></item>
    /// <item><term>Game was stopped</term><description>Players stopped game with empty input</description></item>
    /// </summary>
    public class Game
    {
        private Field _gameField = new Field();
        private Symbol _currPlayer = Symbol.Cross;
        private byte _mandatoryField = 0;
        private bool _gameStopped = false;

        public Symbol Winner 
        { 
            get
            {
                return _gameField.Winner ;
            } 
        }
        public bool IsDraw { get; private set; }

        public Symbol[,] GetGameField ()
        {
            return _gameField.GetFieldStatus();
        }

        public Symbol[,] GetGameGeneralView()
        {
            return _gameField.GetFieldGeneralStatus();
        }

        public bool IsGameStopped
        {
            get
            {
                return _gameStopped;
            }
        }
        /// <summary>
        /// Cross or nought (1 or 2)
        /// </summary>
        public Symbol CurrentPlayer
        {
            get
            {
                return _currPlayer;
            }
        }

        /// <summary>
        /// In case of 0 next turn is "free"
        /// </summary>
        public byte MandatoryField
        {
            get
            {
                return _mandatoryField;
            }
        }

        /// <summary>
        /// Making turn in the game 
        /// </summary>
        /// <param name="userInput">
        /// <para>Input is string that will be converted in byte number</para>
        /// <para>In case of free turn it should be string consist of two digits. First for field, second for cell in this field</para>
        /// <para>In case of mandatory turn only one last digit will be taken from input</para></param>
        /// <para>Game will be stopped if userInput is empty or null</para>
        /// <returns>Code if turn completed or should be repeated for some reason</returns>
        public GameResponseCode MakeTurn(string userInput)
        {
            GameResponseCode response;
            if (string.IsNullOrEmpty(userInput) || _gameStopped)
            {
                _gameStopped = true;
                return GameResponseCode.GameEnded;
            }
            if (_mandatoryField == 0)
            {
                response = MakeFreeTurn(userInput);
            }
            else
            {
                response = MakeMandatoryTurn(userInput);
            }

            if (_gameField.IsDraw)
            {
                _gameStopped = true;
                IsDraw = true;
            } else if (this.Winner != Symbol.Empty)
            {
                _gameStopped = true;
            }

            return response;
        }

        private void ChangePlayer()
        {
            if (_currPlayer == Symbol.Cross)
                _currPlayer = Symbol.Nought;
            else
                _currPlayer = Symbol.Cross;
        }

        private GameResponseCode MakeFreeTurn(string userInput)
        {
            byte number = 0;
            if (byte.TryParse(userInput, out number))
            {
                byte subFieldNumber = (byte)(number / 10);
                byte cellNumber = (byte)(number % 10);

                if (subFieldNumber > 9 || subFieldNumber == 0)
                {
                    return GameResponseCode.RepeatTurnBadFieldNumber;
                }
                if (cellNumber == 0)
                {
                    return GameResponseCode.RepeatTurnBadCellNumber;
                }

                subFieldNumber--;
                cellNumber--;

                if (_gameField.IsSubFieldFull(subFieldNumber))
                {
                    return GameResponseCode.RepeatTurnFullField;
                }

                if (_gameField.TryPutValueByFieldNumber(subFieldNumber, cellNumber, _currPlayer))
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
                    return GameResponseCode.TurnCompleted;
                }
                else
                {
                    return GameResponseCode.RepeatTurnNotEmptyCell;
                }

            }
            else
            {
                return GameResponseCode.RepeatTurnBadInput;
            }
        }

        private GameResponseCode MakeMandatoryTurn(string userInput)
        {
            byte cellNumber = 0;
            if (byte.TryParse(userInput, out cellNumber))
            {
                cellNumber = (byte)(cellNumber % 10);
                if (cellNumber > 9 || cellNumber == 0)
                {
                    return GameResponseCode.RepeatTurnBadCellNumber;
                }

                cellNumber--;
                byte subField = (byte)(_mandatoryField - 1);
                if (_gameField.TryPutValueByFieldNumber(subField, cellNumber, _currPlayer))
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
                    return GameResponseCode.TurnCompleted;
                }
                else
                {
                    return GameResponseCode.RepeatTurnNotEmptyCell;
                }

            }
            else
            {
                return GameResponseCode.RepeatTurnBadInput;
            }
        }
    }
}
