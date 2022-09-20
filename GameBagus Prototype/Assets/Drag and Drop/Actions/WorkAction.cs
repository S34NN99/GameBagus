using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WorkAction : BaseCandleAction {

    public override void ActOn(Candle candle) {
        print(candle.name + " is working");
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Working());
    }
}
