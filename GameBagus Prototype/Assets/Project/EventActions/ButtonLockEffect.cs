using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Effects/Button Lock")]
public class ButtonLockEffect : ScriptableObject {
    [SerializeField] private float _duration = 5f;
    private float Duration => _duration;

    [SerializeField] private string _buttonToLock = "Vacation";
    private string ButtonToLock => _buttonToLock;

    public void StartLockButton(GameObject buttonParent) {
        if (buttonParent == null) {
            buttonParent = GameObject.Find("Action Buttons");
        }

        Button targetButton = buttonParent.GetComponent<Button>();
        targetButton.StartCoroutine(LockButton());

        IEnumerator LockButton() {
            ButtonSwitch(targetButton, false);
            yield return new WaitForSeconds(Duration);
            ButtonSwitch(targetButton, true);
        }
    }

    private void ButtonSwitch(Button targetButton, bool power) {
        targetButton.GetComponent<Button>().interactable = power;
        targetButton.GetComponent<DragHandler>().enabled = power;
    }
}
