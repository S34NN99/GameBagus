using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class M_Happy : MoodState {
    public override string Name => "Happy";

    public override int CurrentIndex => (int)MoodStatesIndex.Happy;

    public override void Enter(IEntity entity) {
        entity.currCandle.UpdateCandleImgCallback.Invoke(entity.currCandle.Skin.GetFacialExpression(this));
        entity.currCandle.SM.powerMod.Strength = 3;
        //CandleSpeech speech = entity.currCandle.GetComponent<CandleSpeech>();
        //entity.currCandle.ShowDialog(speech.GetDialog());
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
            entity.currCandle.SM.SetMoodState(new M_Neutral());
        }
    }
}
