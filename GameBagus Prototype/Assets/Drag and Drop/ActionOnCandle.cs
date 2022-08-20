
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActionOnCandle : MonoBehaviour, IEndDragHandler {
    [SerializeField] private Camera candleCamera;
    [SerializeField] private UnityEvent<Candle> actOnCandleCallback;

    [HideInInspector] public Vector2 positionOnScreen;

    public void OnEndDrag(PointerEventData eventData) {
        Ray ray = candleCamera.ScreenPointToRay(positionOnScreen);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider.TryGetComponent(out Candle candle)) {
                actOnCandleCallback.Invoke(candle);
            }
        }
    }
}