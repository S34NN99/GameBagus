using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleStateLock : MonoBehaviour {
    [SerializeField] private float duration = 5f;
    private WorkingState stateToLock = new W_Crunch();
    private WorkingState defaultState = null;

    public void StartLockState(Candle candle)
    {
        StartCoroutine(LockState(candle));
    }

    private IEnumerator LockState(Candle candle)
    {
        defaultState = candle.SM.workingState;
        candle.SM.SetWorkingState(stateToLock);
        yield return new WaitForSeconds(duration);
        candle.SM.SetWorkingState(defaultState);
    }
}
