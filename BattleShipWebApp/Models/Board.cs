using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipWebApp
{
    public class Board
    {
        public int Size;
        private int previousX = -1;
        private int previousY = -1;
        public List<Ship> Ships = new List<Ship>();
        public List<Hit> MissHits = new List<Hit>();
        public List<List<Cell>> CurrentBoard = new List<List<Cell>>();

        public Board(int size)
        {
            this.Size = size;
            GenerateCurrentBoard();
        }

        public void ShowShipsInCurrentBoard()
        {
            List<Ship> notHitShips = Ships.Where(ship => ship.isHit == false).ToList();
            foreach (Ship ship in notHitShips)
            {
                CurrentBoard[ship.X][ship.Y].Type = CellType.Ship;
            }
        }

        private void GenerateCurrentBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                List<Cell> columns = new List<Cell>();
                for (int j = 0; j < Size; j++)
                {
                    columns.Add(new Cell() { Type = CellType.Sea });
                }
                CurrentBoard.Add(columns);
            }
        }

        public void AddShip(Ship newShip)
        {
            bool isError = false;
            foreach (Ship ship in Ships)
            {
                if (ship.X == newShip.X && ship.Y == newShip.Y)
                {
                    isError = true;
                    break;
                }
                if (newShip.X < 0 || newShip.Y < 0 || newShip.X >= this.Size || newShip.Y >= this.Size)
                {
                    isError = true;
                    break;
                }
                if (newShip.X >= ship.X - 1 && newShip.X <= ship.X + 1 && newShip.Y >= ship.Y - 1 && newShip.Y <= ship.Y + 1)
                {
                    isError = true;
                    break;
                }
            }
            if (!isError)
            {
                Ships.Add(newShip);
            }
        }

        //Performs attack on ships.
        public CellType Attack(int x, int y)
        {
            if (x == previousX && y == previousY)
            {
                throw new Exception("You already shot there!");
            }
            previousX = x;
            previousY = y;
            if (x < 0 || y < 0 || x >= this.Size || y >= this.Size)
            {
                throw new Exception("You can't attack outside the board!");
            }
            else
            {
                Ship foundShip = Ships.FirstOrDefault(ship => ship.X == x && ship.Y == y);
                if (foundShip != null)
                {
                    foundShip.isHit = true;
                    CurrentBoard[x][y].Type = CellType.ShipHit;
                    return CellType.ShipHit;
                }
                else
                {
                    MissHits.Add(new Hit(x, y));
                    CurrentBoard[x][y].Type = CellType.EmptyHit;
                    return CellType.EmptyHit;
                }
            }
        }
        public void GenerateShips(int count)
        {
            Random rnd = new Random();
            int maxLoopCount = count * 2;
            int loop = 0;
            while (Ships.Count < count && loop < maxLoopCount)
            {                        
                AddShip(new Ship(rnd.Next(0, Size), rnd.Next(0, Size)));
                loop++;
            }
            //ShowShipsInCurrentBoard(); // uncomment if you want to test where are ships
        }
    }
}
