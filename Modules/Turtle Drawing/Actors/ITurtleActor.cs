namespace Chinchillada.Generation.Turtle
{
    using System.Collections;
    using System.Collections.Generic;
    using Generation.Textures;
    using UnityEngine;

    public interface ITurtleActor
    {
        IEnumerator ExecuteActions(IEnumerable<ActionType> actions, Color color, ITextureDrawer drawer);
    }
}