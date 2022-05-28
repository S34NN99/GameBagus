using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Crunch : WorkingState
{
    public override string Name => "Crunch";

    public override void Enter(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.Work(pb);
        entity.currCandle.Decay();
    }

    public override void Exit(IEntity entity)
    {
        Debug.Log("Exiting state " + Name);
    }
}
