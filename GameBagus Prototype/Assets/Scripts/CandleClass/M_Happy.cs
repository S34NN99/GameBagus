using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Happy : MoodState
{
    public override string Name => "Happy";

    public override void Enter(IEntity entity)
    {
        Debug.Log("Enter active state " + Name);
    }

    public override void Update(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Exit(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }


}
