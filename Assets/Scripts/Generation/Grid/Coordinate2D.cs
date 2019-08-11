namespace Chinchillada.Generation.CellularAutomata
{
    public class Coordinate2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Get(Grid2D grid) => grid.Items[this.X, this.Y];

        public void Set(int value, Grid2D grid) => grid.Items[this.X, this.Y] = value;
        
        
    }
}