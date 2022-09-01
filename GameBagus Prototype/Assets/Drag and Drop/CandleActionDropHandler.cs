using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class CandleActionDropHandler : MonoBehaviour, IDropHandler {
    [SerializeField] private Candle targetCandle;
    [SerializeField] private Canvas parentCanvas;

    [Tooltip("Delete this after")]
    [SerializeField] private bool candleInUi;
    [SerializeField] private Camera candleCamera;

    [HideInInspector] public Vector2 positionOnScreen;

    public void OnDrop(PointerEventData eventData) {
        print(targetCandle.name);
        if (candleInUi) {
            if (eventData.selectedObject.TryGetComponent(out CandleAction action)) {
                action.ActOn(targetCandle);
            }
        } else {
            candleCamera.ScreenPointToRay(positionOnScreen);
            Ray ray = candleCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.collider.TryGetComponent(out CandleAction action)) {
                    action.ActOn(targetCandle);
                }
            }
        }
    }
}
