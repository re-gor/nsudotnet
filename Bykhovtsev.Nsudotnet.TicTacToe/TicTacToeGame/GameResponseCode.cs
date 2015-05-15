using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bykhovtsev.Nsudotnet.TicTacToe
{
    public enum GameResponseCode
    {
        TurnCompleted,
        RepeatTurnBadCellNumber,
        RepeatTurnNotEmptyCell,
        RepeatTurnFullField,
        RepeatTurnBadFieldNumber,
        RepeatTurnBadInput,
        GameEnded
    }
}
