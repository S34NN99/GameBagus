using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class M_Happy : MoodState {
    public override string Name => "Happy";

    public override int CurrentIndex => (int)MoodStatesIndex.Happy;

    public override void Enter(IEntity entity) {
        entity.currCandle.HeadImage.sprite = entity.currCandle.Skin.GetFacialExpression(this);
        CandleSpeech speech = entity.currCandle.GetComponent<CandleSpeech>();
        speech.ShowDialog(speech.GetDialog());
        Debug.Log("State Speech");
    }

    public override void Update(IEntity entity, ProgressBar pb) {
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

    public override bool CalculateThreshold(IEntity entity) {
        float threshold = entity.currCandle.candleStats.MoodThreshold[CurrentIndex];
        return entity.currCandle.candleStats.HP < threshold;
    }



}
