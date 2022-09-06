using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLock : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private string buttonToLock = "Vacation Button";
    private GameObject button => GameObject.Find(buttonToLock);

    public void StartLockButton()
    {
        StartCoroutine(LockButton());
    }

    private IEnumerator LockButton()
    {
        ButtonSwitch(false);
        yield return new WaitForSeconds(duration);
        ButtonSwitch(true);
    }

    private void ButtonSwitch(bool power)
    {
        button.GetComponent<Button>().interactable = power;
        button.GetComponent<DragHandler>().enabled = power;
    }
}
