namespace BattleShipWebApp
{
    public class Cell
    {
        public CellType Type { get; set; }
    }
    public enum CellType
    {
        Sea,
        EmptyHit,
        Ship, 
        ShipHit
    }
}

