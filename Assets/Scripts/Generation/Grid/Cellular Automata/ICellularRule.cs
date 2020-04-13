namespace Chinchillada.Generation.Grid
{
    public interface ICellularRule
    {
        int Apply(int x, int y, IntGrid2D grid);
    }
}