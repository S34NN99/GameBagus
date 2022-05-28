using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public StateMachine SM { get; }
    public Candle currCandle { get; set; }
}

public abstract class State
{
    public abstract string Name { get; }
    public abstract void Enter(IEntity entity);
    public abstract void Update(IEntity entity, ProgressBar pb);
    public abstract void Exit(IEntity entity);
}

public abstract class WorkingState : State { }

public enum MoodStatesIndex
{
    Happy,
    Neutral, 
    Sad
}

public abstract class MoodState : State 
{
    public abstract int CurrentIndex { get; }
    public abstract void CheckHP(IEntity entity);
    public abstract bool CalculateThreshold(IEntity entity);
}




