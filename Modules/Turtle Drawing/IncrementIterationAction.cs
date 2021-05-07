using System;
using Chinchillada.Behavior;
using Chinchillada.NodeGraph;
using Chinchillada.Generation.Turtle;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class IncrementIterationAction : IAction
{
    [SerializeField] private LSystemJob job;

    [SerializeField] private int value;
    
    public void Trigger()
    {
        this.job.Iterations = ++this.value;
    }

    [Button]
    public void ResetIterations()
    {
        this.value = 0;
    }
}