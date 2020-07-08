using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Mazes
{
    public class GridGraphController : ChinchilladaBehaviour
    {
        [SerializeField] private GridGraphRenderer gridRenderer;

        [SerializeField] private IAsyncGenerator<GridGraph> gridGenerator;

        private void OnGenerated(GridGraph grid)
        {
            this.gridRenderer.Render(grid);
        }
        
        private void OnEnable()
        {
            this.gridGenerator.Generated += OnGenerated;
        }

        private void OnDisable()
        {
            this.gridGenerator.Generated -= OnGenerated;
        }

      
    }
}