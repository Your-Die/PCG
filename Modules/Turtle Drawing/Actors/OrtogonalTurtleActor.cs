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
    public class OrtogonalTurtleActor : ITurtleActor
    {
        [Header("Turtle")] [SerializeField, Range(0, 1)]
        private float startingPointX = 0.5f;

        [SerializeField, Range(0, 1)] private float startingPointY = 0.5f;

        [SerializeField] private Direction startingDirection = Direction.North;
        [SerializeField] private int startingWidth = 0;

        [SerializeField] private int moveDistance = 3;

        [SerializeField] private Vector2Int widthRange = new Vector2Int(0, 5);


        public IEnumerator ExecuteActions(IEnumerable<ActionType> actions, Color color,
                                          ITextureDrawer          drawer)
        {
            var startingPosition = new Vector2Int
            {
                x = (int) (this.startingPointX * drawer.Texture.width),
                y = (int) (this.startingPointY * drawer.Texture.height)
            };

            var memory = new Stack<Turtle>();

            var turtle = new Turtle(startingPosition, this.startingDirection, this.startingWidth, color);

            foreach (var action in actions)
            {
                switch (action)
                {
                    case ActionType.TurnLeft:
                        turtle.RotateLeft();
                        break;
                    case ActionType.TurnRight:
                        turtle.RotateRight();
                        break;
                    case ActionType.MoveForward:
                        this.DrawForwards(ref turtle, drawer);
                        yield return null;
                        break;
                    case ActionType.SkipForward:
                        this.ModeForwards(ref turtle);
                        break;
                    case ActionType.PushMemory:
                        memory.Push(turtle);
                        break;
                    case ActionType.PopMemory:
                        turtle = memory.Pop();
                        break;

                    case ActionType.Grow:
                        this.AdjustSize(ref turtle, 1);
                        break;
                    case ActionType.Shrink:
                        this.AdjustSize(ref turtle, -1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ModeForwards(ref Turtle turtle)
        {
            var direction = turtle.Direction.ToVectorInt();
            var movement  = direction * this.moveDistance;

            turtle.Move(movement);
        }

        private void DrawForwards(ref Turtle turtle, ITextureDrawer drawer)
        {
            var movement   = turtle.Direction.ToVectorInt();
            var orthogonal = turtle.Direction.Clockwise().ToVectorInt();

            for (var center = 0; center < this.moveDistance; center++)
            {
                turtle.Move(movement);
                
                for (var offset = -turtle.Width; offset <= turtle.Width; offset++)
                {
                    var pixel = turtle.Position + offset * orthogonal;
                    drawer.DrawPixel(pixel, turtle.Color);
                }
            }
        }

        private void AdjustSize(ref Turtle turtle, int delta)
        {
            var newWidth = turtle.Width + delta;
            turtle.Width = this.widthRange.RangeClamp(newWidth);
        }


        private struct Turtle
        {
            public readonly Color Color;

            public Vector2Int Position;

            public int Width;

            public Direction Direction { get; private set; }

            public Turtle(Vector2Int position, Direction direction, int width, Color color)
            {
                this.Color     = color;
                this.Position  = position;
                this.Width     = width;
                this.Direction = direction;
            }

            public void Move(Vector2Int movement) => this.Position += movement;

            public void RotateLeft() => this.Direction = this.Direction.CounterClockwise();

            public void RotateRight() => this.Direction = this.Direction.Clockwise();
        }
    }
}