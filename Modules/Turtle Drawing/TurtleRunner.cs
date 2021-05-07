namespace Chinchillada.Turtle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Chinchillada;
    using Generation.Textures;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class TurtleRunner : ChinchilladaBehaviour
    {
        [Header("Draw Speed")] [SerializeField]
        private int actionsPerUpdate = -1;

        [FoldoutGroup("References")] [OdinSerialize, Required, FindComponent(SearchStrategy.InScene)]
        private ITurtleActor actor;

        [OdinSerialize, Required] private ITextureDrawer textureDrawer;

        public bool IsRunning { get; private set; }

        public void RunImmediate(IEnumerable<ActionType> actions, Texture2D texture, Color color)
        {
            this.textureDrawer.Texture = texture;
            var enumerator = this.actor.ExecuteActions(actions, color, this.textureDrawer);

            enumerator.EnumerateFully();

            this.textureDrawer.Texture.Apply();
        }

        public IEnumerator RunRoutine(IEnumerable<ActionType> actions, Texture2D texture, Color color)
        {
            var iterations = RunIteratively(actions, texture, color);
            
            foreach (var _ in iterations)
                yield return null;
        }

        public IEnumerable<Texture2D> RunIteratively(IEnumerable<ActionType> actions, Texture2D texture, Color color)
        {
            this.textureDrawer.Texture = texture;
            var enumerator = this.actor.ExecuteActions(actions, color, this.textureDrawer);

            this.IsRunning = true;
            var stepCount = 0;
            while (enumerator.MoveNext())
            {
                stepCount++;
                if (stepCount != this.actionsPerUpdate)
                    continue;

                this.textureDrawer.Texture.Apply();
                yield return this.textureDrawer.Texture;

                stepCount = 0;
            }

            this.IsRunning = false;
            yield return this.textureDrawer.Texture;
        }
    }
}