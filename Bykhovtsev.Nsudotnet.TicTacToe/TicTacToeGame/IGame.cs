using System;
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
    public interface IGame
    {
        /// <summary>
        /// Cross or nought (1 or 2)
        /// </summary>
        Bykhovtsev.Nsudotnet.TicTacToe.Symbol CurrentPlayer { get; }
        
        /// <summary>
        /// Get all cells of field in format of array 9x9
        /// </summary>
        /// <returns></returns>
        Bykhovtsev.Nsudotnet.TicTacToe.Symbol[,] GetGameField();

        /// <summary>
        /// Get winners of each subfield in format of array 3x3, 
        /// </summary>
        /// <returns></returns>
        Bykhovtsev.Nsudotnet.TicTacToe.Symbol[,] GetGameGeneralView();
        
        /// <summary>
        /// true, if draw
        /// </summary>
        bool IsDraw { get; }

        /// <summary>
        /// true, if game was stopped
        /// </summary>
        bool IsGameStopped { get; }

        /// <summary>
        /// Making turn in the game 
        /// </summary>
        /// <param name="userInput">
        /// <para>Input is string that will be converted in byte number</para>
        /// <para>In case of free turn it should be string consist of two digits. First for field, second for cell in this field</para>
        /// <para>In case of mandatory turn only one last digit will be taken from input</para></param>
        /// <para>Game will be stopped if userInput is empty or null</para>
        /// <returns>Code if turn completed or should be repeated for some reason</returns>
        Bykhovtsev.Nsudotnet.TicTacToe.GameResponseCode MakeTurn(string userInput);

        /// <summary>
        /// In case of 0 next turn is "free". Otherwise it is number of subfield
        /// </summary>
        byte MandatoryField { get; }

        /// <summary>
        /// Winner of game if exists. Symbol.Empty otherwise
        /// </summary>
        Bykhovtsev.Nsudotnet.TicTacToe.Symbol Winner { get; }
    }
}
