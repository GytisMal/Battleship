using System.Collections.Generic;

namespace BattleShipWebApp
{
    public static class GameFlow
    {
        public static Dictionary<string, Game> Games = new Dictionary<string, Game>();
        public static string StartNewGame(string userName, int boardSize, int shipsCount)
        {
            if(!Games.ContainsKey(userName))
            {
                Games.Add(userName, new Game(boardSize, shipsCount));
                return "Game started."; 
            }
            else
            {
                return "Can not start a new Game, there is one already started.";
            }
        }
    }
}