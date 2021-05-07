namespace Turtle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Chinchillada;
    using Chinchillada.Turtle;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEditor.Build.Content;
    using UnityEngine;

    public class PlayerTurtleController : ChinchilladaBehaviour
    {
        [SerializeField] private List<ISource<Color>> colors;

        [OdinSerialize] private Dictionary<KeyCode, IAction> actions;

        [SerializeField] private Context context;

        [FoldoutGroup("References"), OdinSerialize, Required, FindComponent]
        private ISource<Texture2D> textureSource;

        [FoldoutGroup("References"), OdinSerialize, Required, FindComponent]
        private TurtleRunner runner;

        [FoldoutGroup("References"), OdinSerialize, Required, FindComponent(SearchStrategy.InChildren)]
        private IVisualizer<Texture2D> textureVisualizer;        
        
        [FoldoutGroup("References"), OdinSerialize, Required, FindComponent(SearchStrategy.InChildren)]
        private IVisualizer<string> inputVisualizer;

        [FoldoutGroup("References"), OdinSerialize, FindComponent(SearchStrategy.InChildren)]
        private IVisualizer<IEnumerable<string>> previewsVisualizer;

        private RoutineHandler routineHandler;

        protected override void Awake()
        {
            base.Awake();
            this.routineHandler = new RoutineHandler(this);
        }

        private void Update()
        {
            var isDirty = this.UpdateInput();
            if (isDirty)
                this.routineHandler.StartRoutine(UpdateTurtle());
        }

        private IEnumerator UpdateTurtle()
        {
            var texture = this.textureSource.Get();

            var rewrites = new List<string>();

            this.ExecuteInput(texture);

            var system = new LSystem('F', this.context.Input);
            for (var index = 1; index < this.colors.Count; index++)
            {
                var characters = system.Rewrite(this.context.Input, index);

                if (this.previewsVisualizer != null)
                {
                    var rewrite = characters.BuildString();
                    rewrites.Add(rewrite);

                    this.previewsVisualizer.Visualize(rewrites);

                    characters = rewrite;
                }

                var turtleActions = TurtleScanner.Scan(characters);

                var color = this.colors[index].Get();
                
                foreach (var iteration in this.runner.RunIteratively(turtleActions, texture, color))
                {
                    iteration.Apply();
                    this.textureVisualizer.Visualize(iteration);
                    
                    yield return null;
                }
            }
        }

        private void ExecuteInput(Texture2D texture)
        {
            var turtleActions = TurtleScanner.Scan(this.context.Input);
            this.runner.RunImmediate(turtleActions, texture, this.colors[0].Get());
            this.textureVisualizer.Visualize(texture);

            this.inputVisualizer.Visualize(this.context.Input);
        }

        private bool UpdateInput()
        {
            var isDirty = false;

            foreach (var key in this.actions.Keys.Where(Input.GetKeyDown))
            {
                isDirty = true;
                var action = this.actions[key];

                action.Execute(this.context);
            }

            return isDirty;
        }

        [Serializable]
        private class Context
        {
            [SerializeField] private string input;

            public string Input
            {
                get => this.input;
                set => this.input = value;
            }

            public void CopyFrom(Context other)
            {
                this.Input = other.Input;
            }
        }

        private interface IAction
        {
            void Execute(Context context);
        }

        [Serializable]
        private class CharAction : IAction
        {
            [SerializeField] private char character;

            public void Execute(Context context)
            {
                context.Input += this.character;
            }
        }

        [Serializable]
        private class SetContextAction : IAction
        {
            [SerializeField] private Context values;

            public void Execute(Context context)
            {
                context.CopyFrom(this.values);
            }
        }

        [Serializable]
        private class UndoAction : IAction
        {
            public void Execute(Context context)
            {
                var input = context.Input;

                if (!string.IsNullOrEmpty(input))
                    context.Input = input.Substring(0, input.Length - 1);
            }
        }
    }
}