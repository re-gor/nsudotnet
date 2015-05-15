using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    class Field : IField
    {

        private SubField[,] _subFields = new SubField[3, 3];
        public Symbol Winner {get; private set;}
        public bool IsDraw { get; private set; }

        #region Constructors
        public Field()
        {
            Winner = Symbol.Empty;
            IsDraw = false;
            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    _subFields[i, j] = new SubField();
                }
            }
        }
        //Used for debug purposes
        //public Field(SubField sf)
        //{
        //    for (byte i = 0; i < 3; ++i)
        //    {
        //        for (byte j = 0; j < 3; ++j)
        //        {
        //            _subFields[i, j] = sf;
        //        }
        //    }
        //}
        #endregion

        #region Methods
        

        public Symbol GetSubFieldWinner (byte subFieldNumber)
        {
            if (subFieldNumber > 8)
                throw new ArgumentException("subFieldNumber should be less than 9");

            return _subFields[subFieldNumber / 3, subFieldNumber % 3].Winner;
        }

        private void CheckWinner()
        {
            //Check Diagonal lines
            if (_subFields[0, 0].Winner != Symbol.Empty 
                && _subFields[0, 0].Winner == _subFields[1, 1].Winner 
                && _subFields[1, 1].Winner == _subFields[2, 2].Winner)
            {
                Winner = _subFields[0, 0].Winner;
                return;
            }
            else if (_subFields[0, 2].Winner != Symbol.Empty
                && _subFields[0, 2].Winner == _subFields[1, 1].Winner
                && _subFields[1, 1].Winner == _subFields[2, 0].Winner)
            {
                Winner = _subFields[0, 2].Winner;
                return;
            };

            //Check vertical and horizontal lines
            for (byte i = 0; i < 3; i++)
            {
                if (_subFields[i, 0].Winner != Symbol.Empty
                    && _subFields[i, 0].Winner == _subFields[i, 1].Winner
                    && _subFields[i, 1].Winner == _subFields[i, 2].Winner)
                {
                    Winner = _subFields[i, 0].Winner;
                    return;
                }
                else if (_subFields[0, i].Winner != Symbol.Empty
                    && _subFields[0, i].Winner == _subFields[1, i].Winner
                    && _subFields[1, i].Winner == _subFields[2, i].Winner)
                {
                    Winner = _subFields[0, i].Winner;
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
                    if (_subFields[i,j].IsDraw)
                    {
                        rowDraw = true;
                        break;
                    }

                    if (_subFields[i, j].Winner != Symbol.Empty)
                    {
                        if (pretender == Symbol.Empty)
                            pretender = _subFields[i, j].Winner;
                        else
                        {
                            if (pretender != _subFields[i, j].Winner)
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
                        if (_subFields[i, j].IsDraw)
                        {
                            colDraw = true;
                            break;
                        }
                        if (_subFields[i, j].Winner != Symbol.Empty)
                        {
                            if (pretender == Symbol.Empty)
                                pretender = _subFields[i, j].Winner;
                            else
                            {
                                if (pretender != _subFields[i, j].Winner)
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
            if (IsDraw)
            {
                if (_subFields[1, 1].IsDraw)
                {
                    return;
                }
                Symbol pretender = Symbol.Empty;
                bool diagDraw = false;
                for (byte i = 0; i < 3; ++i)
                {
                    if (_subFields[i, i].IsDraw)
                    {
                        diagDraw = true;
                        break;
                    }

                    if (_subFields[i, i].Winner != Symbol.Empty)
                    {
                        if (pretender == Symbol.Empty)
                            pretender = _subFields[i, i].Winner;
                        else
                        {
                            if (pretender != _subFields[i, i].Winner)
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
                        if (_subFields[i, 2 - i].Winner != Symbol.Empty)
                        {
                            if (pretender == Symbol.Empty)
                                pretender = _subFields[i, 2 - i].Winner;
                            else
                            {
                                if (pretender != _subFields[i, 2 - i].Winner)
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

        public bool IsSubFieldFull(byte subFieldNumber)
        {
            if (subFieldNumber > 8)
                throw new ArgumentException("subFieldNumber should be less than 9");

            return _subFields[subFieldNumber / 3, subFieldNumber % 3].IsFull;
        }

        public bool TryPutValueByFieldNumber(byte subFieldNumber, byte cellNumber, Symbol value)
        {
            if (subFieldNumber > 8)
                throw new ArgumentException("subFieldNumber should be less than 9");
            if (cellNumber > 8)
                throw new ArgumentException("cellNumber should be less than 9");

            return TryPutValue((byte)((subFieldNumber / 3) * 3 + cellNumber / 3), (byte)(3 * (subFieldNumber % 3) + cellNumber % 3), value);
        }
       
        public bool TryPutValue(byte rowNumber, byte colNumber, Symbol value)
        {
            if (colNumber > 9 || rowNumber > 9) 
                throw new ArgumentException("Numbers should be less than 9");
            if (_subFields[colNumber / 3, rowNumber / 3].IsFull)
                throw new Exception("Choosen subField is full, you can not put value to it");

            if (_subFields[rowNumber / 3, colNumber / 3].GetCellValue((byte)(rowNumber % 3), (byte)(colNumber % 3)) != Symbol.Empty)
                return false;

            _subFields[rowNumber / 3, colNumber / 3].TryPutValue((byte)(rowNumber % 3), (byte)(colNumber % 3), value);

            if (Winner == Symbol.Empty)
                CheckDraw();

            if (!IsDraw && Winner == Symbol.Empty)
                CheckWinner();

            return true;
        }
        
        public Symbol [,] GetFieldStatus()
        {
            Symbol[,] result = new Symbol[9,9];

            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    for (byte k = 0; k < 3; ++k)
                    {
                        for (byte l = 0; l < 3; ++l)
                        {
                            result[k + 3 * i, l + 3 * j] = _subFields[i, j].GetCellValue(k, l);
                        }
                    }
                }
            }

            return result;
        }
        public Symbol[,] GetFieldGeneralStatus()
        {
            Symbol[,] result = new Symbol[3, 3];
            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    result[i, j] = _subFields[i, j].Winner;
                }
            }
            return result;
        }

        public void Paint ()
        {
            string delimeter = "\n===== || ===== || =====\n";
            Console.WriteLine("====Detailed View====\n");
            for (byte i = 0; i < 3; ++i)
            {
                for (byte k = 0; k < 3; ++k )
                {
                    for (byte j = 0; j < 3; ++j )
                    {
                        Console.Write("{0}", _subFields[i,j].GetRowString(k));
                        if (j < 2)
                        {
                            Console.Write(" || ");
                        }
                        if (j==2)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                if (i < 2)
                {
                    Console.WriteLine(delimeter);
                }
            }
            Console.WriteLine("\n===Global View===");
            delimeter = "\n- - -";
            for (byte i = 0; i < 3; ++i)
            {
                for (byte j = 0; j < 3; ++j)
                {
                    switch (_subFields[i,j].Winner)
                    {
                        case Symbol.Cross:
                            Console.Write("X");
                            break;
                        case Symbol.Nought:
                            Console.Write("O");
                            break;
                        case Symbol.Empty:
                            Console.Write(" ");
                            break;
                    }
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

        #endregion
    }
}
