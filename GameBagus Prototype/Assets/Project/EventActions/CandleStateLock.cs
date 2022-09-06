using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleStateLock : MonoBehaviour {

    [SerializeField] private CandleManager CM;
    [SerializeField] private float duration = 5f;

    private WorkingState stateToLock = new W_Crunch();
    private WorkingState defaultState = null;

    public void StartLockState()
    {
        StartCoroutine(LockState(CM.RandomizeCandle()));
    }

    private IEnumerator LockState(Candle candle)
    {
        defaultState = candle.SM.workingState;
        candle.SM.SetWorkingState(stateToLock);
        yield return new WaitForSeconds(duration);
        candle.SM.SetWorkingState(defaultState);
    }
}
