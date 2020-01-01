using System.Collections;
using Chinchillada.Generation.Grid;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Evolution.Grid
{
    public class EvolutionRenderer : ChinchilladaBehaviour
    {
        [SerializeField] private float updateInterval = 0.01f;
        
        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private GridEvolution evolution;
        
        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private IGridRenderer gridRenderer;

        private IEnumerator routine;
        
        [Button]
        public void RunEvolution()
        {
            if (this.routine != null) 
                this.StopCoroutine(this.routine);

            this.routine = this.EvolutionRoutine();
            this.StartCoroutine(this.routine);
        }

        private IEnumerator EvolutionRoutine()
        {
            foreach (var genotype in this.evolution.EvolveGenerationWise())
            {
                var grid = genotype.Candidate;
                Debug.Log(genotype.Fitness);
                
                this.gridRenderer.Render(grid);
                
                yield return new WaitForSeconds(this.updateInterval);
            }
        }
    }
}