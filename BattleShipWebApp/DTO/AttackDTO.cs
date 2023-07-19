namespace BattleShipWebApp
{
    public class AttackPerformDTO
    {
       public string UserName {  get; set; }
       public int X { get; set; }
       public int Y { get; set; }
    }

    public class AttackReturnDTO
    {
        public string Information { get; set; }
        public int? CellType { get; set; }
        public int Ships { get; set; }
    }

}
