using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public MoodState moodState;
    public WorkingState workingState;
    public IEntity owner;

    public StateMachine(IEntity owner)
    {
        this.owner = owner;
    }

    public void SetWorkingState(WorkingState state)
    {
        if (workingState == null && state != null)
        {
            workingState = state;
            workingState.Enter(owner);
        }
    }

    public void SetMoodState(MoodState state)
    {
        if (moodState == null && state != null)
        {
            moodState = state;
            moodState.Enter(owner);
        }
        else if(moodState != null)
        {
            moodState = state;
            moodState.Enter(owner);
        }
    }

    public void UpdateStates(ProgressBar pb)
    {
        moodState.Update(owner, pb);
        workingState.Update(owner, pb);
    }
}
