using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PhoneCallAlert : MonoBehaviour {
    [SerializeField] private CandleMessage _message;
    public CandleMessage Message => _message;

    [Space]
    [SerializeField] private GameObject _actionButtonTemplate;
    private GameObject ActionButtonTemplate => _actionButtonTemplate;

    [SerializeField] private RectTransform _actionButtonParent;
    private RectTransform ActionButtonParent => _actionButtonParent;

    private List<Button> ActionButtons { get; } = new();

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void SetActions(IReadOnlyList<ProjectEventAction> actions) {
        int newButtonsRequired = actions.Count - ActionButtons.Count;
        for (int i = 0; i < newButtonsRequired; i++) {
            Button actionButton = Instantiate(ActionButtonTemplate, ActionButtonParent).GetComponent<Button>();

            ActionButtons.Add(actionButton);
        }

        // Sets the correct number of buttons active
        for (int i = 0; i < actions.Count; i++) {
            ProjectEventAction eventAction = actions[i];
            Button actionButton = ActionButtons[i];

            if (!actionButton.gameObject.activeSelf) {
                actionButton.gameObject.SetActive(true);
            }

            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(eventAction.EventCallback.Invoke);
            actionButton.onClick.AddListener(Hide);

            actionButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = eventAction.ButtonTitleText;
        }

        // Sets the rest to be inactive
        for (int i = ActionButtons.Count - 1; i >= actions.Count; i--) {
            if (ActionButtons[i].gameObject.activeSelf) {
                ActionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}