namespace Chinchillada.Turtle
{
    using System.Collections.Generic;

    public static class TurtleScanner
    {
        public static IEnumerable<ActionType> Scan(IEnumerable<char> characters)
        {
            var memoryCount = 0;

            foreach (var character in characters)
            {
                switch (character)
                {
                    case '+':
                        yield return ActionType.TurnRight;
                        break;
                    case '-':
                        yield return ActionType.TurnLeft;
                        break;

                    case 'F': yield return ActionType.MoveForward;
                        break;
                    case 'f': yield return ActionType.SkipForward;
                        break;
                    
                    case '<': yield return ActionType.Grow;
                        break;
                    case '>': yield return ActionType.Shrink;
                        break;
                    
                    case '[':
                        memoryCount++;
                        yield return ActionType.PushMemory;
                        break;
                    case ']':
                        if (memoryCount <= 0)
                            continue;

                        memoryCount--;
                        yield return ActionType.PopMemory;
                        break;
                }
            }
        }
    }

    public enum ActionType
    {
        TurnLeft,
        TurnRight,
        MoveForward,
        SkipForward,
        PushMemory,
        PopMemory,
        Grow,
        Shrink
    }
}