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
        if (CalculateThreshold(entity)) {
            entity.currCandle.SM.moodState.Exit(entity);
            entity.currCandle.Death();
        }
    }

    public override bool CalculateThreshold(IEntity entity) {
        //float threshold = entity.currCandle.candleStats.MoodThreshold[CurrentIndex];
        //return entity.currCandle.candleStats.HP < threshold;
        float threshold = entity.currCandle.Stats.MoodThreshold[CurrentIndex];
        return entity.currCandle.Stats.HpProp.Value < threshold;
    }
}
