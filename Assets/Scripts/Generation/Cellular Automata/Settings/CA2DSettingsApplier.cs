namespace Chinchillada.Generation.CellularAutomata
{
    using Grid;
    using Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class CA2DSettingsApplier : ChinchilladaBehaviour
    {
        [SerializeField] private CA2DSettings settings;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private Grid2DGeneratorComponent gridGenerator;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private CellularAutomataGenerator automataGenerator;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private CellularAutomataComponent automata;

        [Button]
        public void ApplySettings()
        {
            this.settings.Apply(this.gridGenerator, this.automataGenerator, this.automata);
            
            this.gridGenerator.SendMessage("OnValidate");
        }

        [Button]
        public void Generate()
        {
            this.ApplySettings();
            this.automataGenerator.Generate();
        }
}
}