using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    [Space]
    [SerializeField] private bool snapToOriginalPos = true;
    [SerializeField] private UnityEvent<Vector2> onPositionOnScreenChanged;

    private Vector2 originalPosition;

    private void Start() {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnButtonPickedUp);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
        onPositionOnScreenChanged.Invoke(rectTransform.anchoredPosition);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (snapToOriginalPos) {
            rectTransform.anchoredPosition = originalPosition;
            onPositionOnScreenChanged.Invoke(rectTransform.anchoredPosition);
        }
        canvasGroup.blocksRaycasts = true;
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnButtonDropped);
    }
}
