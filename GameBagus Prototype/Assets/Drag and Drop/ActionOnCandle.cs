
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActionOnCandle : MonoBehaviour, IEndDragHandler {
    [SerializeField] private Camera candleCamera;
    [SerializeField] private UnityEvent<Candle> actOnCandleCallback;

    public void OnEndDrag(PointerEventData eventData) {
        Ray ray = candleCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider.TryGetComponent(out Candle candle)) {
                actOnCandleCallback.Invoke(candle);
            }
        }
    }
}