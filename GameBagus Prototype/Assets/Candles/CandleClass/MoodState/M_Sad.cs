using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class M_Sad : MoodState {
    public override string Name => "Sad";

    public override int CurrentIndex => (int)MoodStatesIndex.Sad;

    public override void Enter(IEntity entity) {
        entity.currCandle.UpdateCandleImgCallback.Invoke(entity.currCandle.Skin.GetFacialExpression(this));
        entity.currCandle.SM.powerMod.Strength = 1;
        CandleSpeech speech = entity.currCandle.GetComponent<CandleSpeech>();
        entity.currCandle.ShowDialog(speech.GetDialog());

        GeneralEventManager.Instance.BroadcastEvent(AudioManager.NearingCandleBurnoutEvent);
    }

    public override void Update(IEntity entity, Project pb) {
        CheckHP(entity);
    }

    public override void Exit(IEntity entity) {
        entity.currCandle.SM.moodState = null;
    }

    public override void CheckHP(IEntity entity) {
        switch (CalculateThreshold(entity)) {
            case -1:
                entity.currCandle.SM.moodState.Exit(entity);
                entity.currCandle.Death();
                break;
            case 1:
                entity.currCandle.SM.moodState.Exit(entity);
                entity.currCandle.SM.SetMoodState(new M_Neutral());
                break;
        }
    }
}
