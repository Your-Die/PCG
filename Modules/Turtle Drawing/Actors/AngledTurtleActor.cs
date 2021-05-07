namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Chinchillada;
    using Generation.Textures;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class AngledTurtleActor : ITurtleActor
    {
        [SerializeField] private float rotationDegrees = 30;

        [Header("Turtle")] [SerializeField, Range(0, 1)]
        private float startingPointX = 0.5f;

        [SerializeField, Range(0, 1)] private float startingPointY = 0.5f;

        [SerializeField, Range(0, 360)] private float startingAngle = 90;

        [SerializeField] private int stepDistance = 4;

        public IEnumerator ExecuteActions(IEnumerable<ActionType> actions, Color color, ITextureDrawer drawer)
        {
            var startingPosition = new Vector2Int
            {
                x = (int) (this.startingPointX * drawer.Texture.width),
                y = (int) (this.startingPointY * drawer.Texture.height)
            };

            var turtle = new Turtle(startingPosition, this.startingAngle, this.stepDistance, color);

            var memory = new Stack<Turtle>();

            foreach (var action in actions)
            {
                switch (action)
                {
                    case ActionType.TurnLeft:
                        turtle.Rotate(-this.rotationDegrees);
                        break;
                    case ActionType.TurnRight:
                        turtle.Rotate(this.rotationDegrees);
                        break;
                    case ActionType.MoveForward:
                        this.DrawForwards(ref turtle, drawer);
                        yield return null;
                        break;
                    case ActionType.SkipForward:
                        this.SkipForwards(ref turtle);
                        break;
                    case ActionType.PushMemory:
                        memory.Push(turtle);
                        break;
                    case ActionType.PopMemory:
                        turtle = memory.Pop();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void DrawForwards(ref Turtle turtle, ITextureDrawer drawer)
        {
            var targetPosition = turtle.GetNextPosition();

            drawer.DrawLine(turtle.position, targetPosition, turtle.color);

            turtle.position = targetPosition;
        }

        private void SkipForwards(ref Turtle turtle) => turtle.SkipForwards();

        private struct Turtle
        {
            public  Vector2Int position;
            private float      degrees;

            private readonly int   distance;
            public readonly  Color color;

            public Turtle(Vector2Int position, float degrees, int distance, Color color)
            {
                this.position = position;
                this.degrees  = degrees;
                this.distance = distance;

                this.color = color;
            }

            public void Rotate(float degreesChange)
            {
                this.degrees += degreesChange;
            }

            public void SkipForwards()
            {
                this.position = this.GetNextPosition();
            }

            public Vector2Int GetNextPosition()
            {
                var direction = DegreeToVector2(this.degrees);
                var delta     = direction * this.distance;

                var targetPosition = (this.position + delta).ToInt();
                return targetPosition;
            }

            public static Vector2 RadianToVector2(float radian)
            {
                return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            }

            public static Vector2 DegreeToVector2(float degree)
            {
                return RadianToVector2(degree * Mathf.Deg2Rad);
            }
        }
    }
}