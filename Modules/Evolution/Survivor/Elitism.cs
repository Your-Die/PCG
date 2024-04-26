using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Algorithms;
using UnityEngine;

namespace Chinchillada.PCG.Evolution.Survivor
{
    [Serializable]
    public class Elitism : ISurvivorSelector
    {
        [SerializeField] private int eliteCount;

        private static readonly GenotypeComparer GenotypeComparer = new GenotypeComparer();
        
        /// <summary>
        /// Selects survivors for the next generation.
        /// </summary>
        /// <returns>A Fitness-sorted selection of survivors.</returns>
        /// <remarks>
        /// Assumes the <paramref name="parents"/> and <paramref name="children"/> collections are sorted by fitness.
        /// </remarks>
        public IEnumerable<IGenotype> SelectSurvivors(IEnumerable<IGenotype> parents, IEnumerable<IGenotype> children)
        {
            var orderedParents = parents.OrderByDescending(parent => parent.Fitness);
            var elite = orderedParents.Take(this.eliteCount);

            return MergeSort.Merge(children, elite, GenotypeComparer);
        }
    }
}