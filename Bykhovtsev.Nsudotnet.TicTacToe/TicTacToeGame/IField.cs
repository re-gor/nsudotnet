using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    interface IField
    {
        Symbol Winner { get; }
        bool IsDraw { get; }

        Symbol[,] GetFieldStatus();  
        //void CheckWinner();
        //void CheckDraw();

    }
}
