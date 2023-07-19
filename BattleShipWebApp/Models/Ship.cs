namespace BattleShipWebApp
{
    public class Ship
    {
        public int X;
        public int Y;
        public bool isHit;

        public Ship(int coordinateX, int coordinateY)
        {
            this.X = coordinateX;
            this.Y = coordinateY;
            this.isHit = false;
        }
    }
}
