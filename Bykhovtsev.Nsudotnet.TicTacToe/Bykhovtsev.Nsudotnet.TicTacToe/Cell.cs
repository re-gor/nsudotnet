using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    struct Cell
    {
        const string CrossSymbol = "X";
        const string Nought = "O";
        private Symbol _value;// = Symbol.Empty;

        public Symbol Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != Symbol.Empty)
                    throw new InvalidOperationException("Cell value is not empty. You can not put another value");

                _value = value;
            }
        }

       

        public override string ToString()
        {
            switch (_value)
            {
                case Symbol.Cross:
                    return CrossSymbol;
                case Symbol.Nought:
                    return Nought;
                case Symbol.Empty:
                    return " ";
            }
            return "?";
        }

        public static bool operator ==(Cell c1, Cell c2)
        {
            return c1.Value == c2.Value;
        }
        public static bool operator !=(Cell c1, Cell c2)
        {
            return c1.Value != c2.Value;
        }
        public static bool operator ==(Cell c1, Symbol c2)
        {
            return c1.Value == c2;
        }
        public static bool operator !=(Cell c1, Symbol c2)
        {
            return c1.Value != c2;
        }
        public static bool operator ==(Symbol c2, Cell c1)
        {
            return c1.Value == c2;
        }
        public static bool operator !=(Symbol c2, Cell c1)
        {
            return c1.Value != c2;
        }
        public override bool Equals(object obj)
        {
            if (obj is Cell)
                return this.Value == ((Cell)obj).Value;

            return false;
        }
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
