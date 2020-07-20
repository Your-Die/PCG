using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Generation.Grid
{
    public class GridRowAccessor : GridListAccessor
    {
        public int RowIndex { get; set; }

        public override int Count => this.Grid.Width;

        public override int this[int index]
        {
            get => this.Grid[index, this.RowIndex];
            set => this.Grid[index, this.RowIndex] = value;
        }

        public GridRowAccessor(Grid2D grid) : base(grid)
        {
        }
    }
}