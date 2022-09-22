using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleStateLock : MonoBehaviour {

    public enum StateToLock
    {
        Crunch,
        Work,
        Vacation
    }

    [SerializeField] private GameObject candleParents;
    [SerializeField] private float duration = 5f;
    [SerializeField] private StateToLock stateToLock;


    private WorkingState W_stateToLock = null;
    private WorkingState defaultState = null;

    public void StartLockState(int childNumber)
    {
        Candle candle = candleParents.transform.GetChild(childNumber - 1).GetComponent<Candle>();
        InitializeState();
        StartCoroutine(LockState(candle));
    }

    void InitializeState()
    {
        switch(stateToLock)
        {
            case StateToLock.Crunch:
                W_stateToLock = new W_Crunch();
                break;

            case StateToLock.Work:
                W_stateToLock = new W_Working();
                break;

            case StateToLock.Vacation:
                W_stateToLock = new W_Vacation();
                break;
        }
    }

    private IEnumerator LockState(Candle candle)
    {
        BoxCollider collider = candle.gameObject.GetComponent<BoxCollider>();
        defaultState = candle.SM.workingState;

        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(W_stateToLock);
        collider.enabled = false;

        yield return new WaitForSeconds(duration);

        collider.enabled = true;
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(defaultState);
    }
}
