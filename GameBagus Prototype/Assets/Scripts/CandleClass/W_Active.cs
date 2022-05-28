using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Active : WorkingState
{
    public override string Name => "Active";

    public override void Enter(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Update(IEntity entity)
    {
        Debug.Log("Updating state " + Name);
    }

    public override void Exit(IEntity entity)
    {
        Debug.Log("Exiting state " + Name);
    }
}
