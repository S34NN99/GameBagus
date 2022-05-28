using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Vacation : WorkingState
{
    public override string Name => "Vacation";

    public override void Enter(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.Regeneration();
    }

    public override void Exit(IEntity entity)
    {
        CheckHP(entity);
        entity.currCandle.SM.workingState = null;
        Debug.Log("Exiting state " + Name);
    }

    public void CheckHP(IEntity entity)
    {
        List<int> threshold = entity.currCandle.candleStats.MoodThreshold;
        for(int i = 0; i < threshold.Count; i++)
        {
            if(CalculateThreshold(entity, i))
            {
                Debug.Log("True" + i);
                switch (i)
                {
                    case (int)MoodStatesIndex.Happy:
                        entity.SM.moodState.Exit(entity);
                        entity.SM.SetMoodState(new M_Happy());
                        return;

                    case (int)MoodStatesIndex.Neutral:
                        entity.SM.moodState.Exit(entity);
                        entity.SM.SetMoodState(new M_Neutral());
                        return;

                    case (int)MoodStatesIndex.Sad:
                        entity.SM.moodState.Exit(entity);

                        entity.SM.SetMoodState(new M_Sad());
                        return;
                }
                
            }
        }
    }

    public bool CalculateThreshold(IEntity entity, int num)
    {
        float threshold = entity.currCandle.candleStats.MoodThreshold[num];
        Debug.Log(threshold + " is the " + num);
        return entity.currCandle.candleStats.HP > threshold;
    }
}
