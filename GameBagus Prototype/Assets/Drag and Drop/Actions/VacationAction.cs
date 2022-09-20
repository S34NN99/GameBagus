using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VacationAction : BaseCandleAction {

    public override void ActOn(Candle candle) {
        print(candle.name + " is on vacation");
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Vacation());
    }
}
