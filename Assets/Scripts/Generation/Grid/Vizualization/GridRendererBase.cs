using System;

namespace Chinchillada.Generation.Grid
{
    public abstract class GridRendererBase : ChinchilladaBehaviour, IGridRenderer
    {
        public IntGrid2D Grid { get; private set; }
        
        public event Action<IntGrid2D> NewGridRegistered;

        public void Render(IntGrid2D grid)
        {
            this.Grid = grid;
            this.NewGridRegistered?.Invoke(grid);
            
            this.RenderGrid(grid);
        }

        protected abstract void RenderGrid(IntGrid2D newGrid);
    }
}