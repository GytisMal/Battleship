namespace BattleShipWebApp
{
    public class Game
    {
        public Board battleBoard;
        public Game(int boardSize, int shipsCount)
        {
            this.battleBoard = new Board(boardSize); 
            battleBoard.GenerateShips(shipsCount);
        }
    }
}