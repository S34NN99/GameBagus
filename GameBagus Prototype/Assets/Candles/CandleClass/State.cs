using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IEntity {
    public StateMachine SM { get; }
    public Candle currCandle { get; set; }
}

[System.Serializable]
public abstract class State {
    public abstract string Name { get; }
    public abstract void Enter(IEntity entity);
    public abstract void Update(IEntity entity, Project pb);
    public abstract void Exit(IEntity entity);
}

[System.Serializable]
public abstract class WorkingState : State {
    public abstract float SimSpeed { get; }
    public abstract float FireSize { get; }
    public abstract float FireSpeed { get; }

    protected void CheckHP(IEntity entity) {
        IReadOnlyList<int> threshold = entity.currCandle.Stats.MoodThreshold;
        //List<int> threshold = entity.currCandle.candleStats.MoodThreshold;
        for (int i = 0; i < threshold.Count; i++) {
            if (CalculateThreshold(entity, i)) {
                switch (i) {
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

    protected bool CalculateThreshold(IEntity entity, int num) {
        float threshold = entity.currCandle.Stats.MoodThreshold[num];
        return entity.currCandle.Stats.HpProp.Value > threshold;
    }
}

public enum MoodStatesIndex {
    Happy,
    Neutral,
    Sad
}

public abstract class MoodState : State {
    public abstract int CurrentIndex { get; }
    public abstract void CheckHP(IEntity entity);

    public virtual int CalculateThreshold(IEntity entity) {
        float lowerBound = entity.currCandle.Stats.MoodThreshold[CurrentIndex + 1];
        float upperBound = entity.currCandle.Stats.MoodThreshold[CurrentIndex];

        float currentHealth = entity.currCandle.Stats.HpProp.Value;
        if (currentHealth < lowerBound) {
            return -1;
        } else if (currentHealth >= lowerBound && currentHealth < upperBound) {
            return 0;
        } else {
            return 1;
        }
    }

}




