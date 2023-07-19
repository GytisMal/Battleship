using System.Collections.Generic;

namespace BattleShipWebApp
{
    public class BoardDTO
    {
        public int ShipsCount { get; set; }
        public List<List<Cell>> CurrentBoard { get; set; }
    }
}