using System.Collections;

using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Candle State Lock")]
public class CandleStateLockEffect : CandleEffect {
    private enum CandleState {
        Crunch,
        Work,
        Vacation
    }

    [SerializeField] private float _duration = 5f;
    public float Duration => _duration;

    [SerializeField] private CandleState _lockState;
    private CandleState LockState => _lockState;

    protected override IEnumerator AffectCandleCoroutine(Candle candle) {
        BoxCollider collider = candle.gameObject.GetComponent<BoxCollider>();
        WorkingState defaultState = candle.SM.workingState;

        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(InitializeState());
        collider.enabled = false;

        yield return new WaitForSeconds(Duration);

        collider.enabled = true;
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(defaultState);
    }

    private WorkingState InitializeState() {
        return LockState switch {
            CandleState.Crunch => new W_Crunch(),
            CandleState.Work => new W_Working(),
            CandleState.Vacation => new W_Vacation(),
            _ => new W_Working(),
        };
    }
}
