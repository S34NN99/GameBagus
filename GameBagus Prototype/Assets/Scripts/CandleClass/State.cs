using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public StateMachine SM { get; }
}

public abstract class State
{
    public abstract string Name { get; }
    public abstract void Enter(IEntity entity);
    public abstract void Update(IEntity entity);
    public abstract void Exit(IEntity entity);
}

public abstract class WorkingState : State { }

public abstract class MoodState : State { }




