using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    class SubField : IField
    {
        

        #region Fields
        private Cell[,] _cells = new Cell[3,3];
        private byte _fullCellsCounter = 0;

        public bool IsFull { get; private set; }  
        public Symbol Winner {get; private set;}
        public bool IsDraw { get; private set; }
        

        #endregion

        #region Constructors
        public SubField ()
        {
            Winner = Symbol.Empty;
            IsDraw = false;
            IsFull = false;
            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    _cells[i,j] = new Cell();
                }
                
            }
        }
        #endregion

        #region Methods
        private void CheckWinner()
        {
            //Check Diagonal lines
            if (_cells[0, 0] != Symbol.Empty && _cells[0, 0] == _cells[1, 1] && _cells[1, 1] == _cells[2, 2])
            {
                Winner = _cells[0, 0].Value;
                return;
            }
            else if (_cells[0, 2] != Symbol.Empty && _cells[0, 2] == _cells[1, 1] && _cells[1, 1] == _cells[2, 0])
            {
                Winner = _cells[0, 2].Value;
                return;
            };
            
            //Check vertical and horizontal lines
            for (byte i = 0; i < 3; i++)
            {
                if (_cells[i, 0] != Symbol.Empty && _cells[i, 0] == _cells[i, 1] && _cells[i, 1] == _cells[i, 2])
                {
                    Winner = _cells[i, 0].Value;
                    return;
                }
                else if (_cells[0, i] != Symbol.Empty && _cells[0, i] == _cells[1, i] && _cells[1, i] == _cells[2, i])
                {
                    Winner = _cells[0, i].Value;
                    return;
                };
            };

            return;
        }

        private void CheckDraw()
        {
            IsDraw = true;
            //check horizontal lines
            for (byte i = 0; i < 3; ++i)
            {
                Symbol pretender = Symbol.Empty;
                bool rowDraw = false;
                for (byte j = 0; j < 3; ++j)
                {
                    if (_cells[i, j] != Symbol.Empty)
                    {
                        if (pretender == Symbol.Empty)
                            pretender = _cells[i, j].Value;
                        else
                        {
                            if (pretender != _cells[i, j])
                            {
                                rowDraw = true;
                                break; 
                            }

                        }
                    }
                }
                if (!rowDraw)
                {
                    IsDraw = false;
                    break;
                }
            }
            //Check Vertical lines if still is draw 
            if (IsDraw)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    Symbol pretender = Symbol.Empty;
                    bool colDraw = false;
                    for (byte i = 0; i < 3; ++i)
                    {
                        if (_cells[i, j] != Symbol.Empty)
                        {
                            if (pretender == Symbol.Empty)
                                pretender = _cells[i, j].Value;
                            else
                            {
                                if (pretender != _cells[i, j])
                                {
                                    colDraw = true;
                                    break;
                                }

                            }
                        }
                    }
                    if (!colDraw)
                    {
                        IsDraw = false;
                        break;
                    }
                }
            }
            //Check Diagonal lines if still is draw 
            if(IsDraw)
            {
                Symbol pretender = Symbol.Empty;
                bool diagDraw = false;
                for (byte i = 0; i < 3; ++i)
                {
                    if (_cells[i, i] != Symbol.Empty)
                    {
                        if (pretender == Symbol.Empty)
                            pretender = _cells[i, i].Value;
                        else
                        {
                            if (pretender != _cells[i, i])
                            {
                                diagDraw = true;
                                break;
                            }

                        }
                    }
                }
                if (diagDraw)
                {
                    diagDraw = false;
                    for (byte i = 0; i < 3; ++i)
                    {
                        if (_cells[i, 2 - i] != Symbol.Empty)
                        {
                            if (pretender == Symbol.Empty)
                                pretender = _cells[i, 2 - i].Value;
                            else
                            {
                                if (pretender != _cells[i, 2 - i])
                                {
                                    diagDraw = true;
                                    break;
                                }

                            }
                        }
                    }
                }

                if (!diagDraw)
                {
                    IsDraw = false;
                }
            }
        }

        public bool TryPutValue (byte cellNumber, Symbol value)
        {
            if (IsFull)
                return false;
                //throw new InvalidOperationException ("SubField is full. You can not put value into it");
            if (cellNumber > 8)
                throw new ArgumentOutOfRangeException("Number of cell should be less than 8");

            if (_cells[cellNumber / 3, cellNumber % 3].Value != Symbol.Empty)
                return false;

            _cells[cellNumber / 3, cellNumber % 3].Value = value;

            if (Winner == Symbol.Empty)
                CheckDraw();

            if (!IsDraw && Winner == Symbol.Empty)
                CheckWinner();

            _fullCellsCounter++;
            if (_fullCellsCounter == 9)
            {
                IsFull = true;
                //if (Winner == Symbol.Empty) //field is full and there still no winner
                    //IsDraw = true;
            }

            return true;
        }

        public Symbol GetCellValue (byte cellNumber)
        {
            if (cellNumber > 8)
                throw new ArgumentOutOfRangeException("Number of cell should be less than 8");

            return _cells[cellNumber / 3, cellNumber % 3].Value;
        }

        public string GetRowString (byte rowNumber)
        {
            return String.Format("{0}|{1}|{2}", _cells[rowNumber, 0], _cells[rowNumber, 1], _cells[rowNumber, 2]);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(30);
            string rowDelimeter = "-----\n";

            sb.Append(this.GetRowString(0) + "\n");
            sb.Append(rowDelimeter);
            sb.Append(this.GetRowString(1) + "\n");
            sb.Append(rowDelimeter);
            sb.Append(this.GetRowString(2) + "\n");

            return sb.ToString();
        }
        #endregion
    }
}
