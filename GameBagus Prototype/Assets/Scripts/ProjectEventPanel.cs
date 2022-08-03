using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ProjectEventPanel : MonoBehaviour {

    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI detailsText;

    public void SetContent(string title, string mainBody, string closing) {
        titleText.text = title;
        detailsText.text = mainBody;
    }

    public void ShowPanel() {
        panelObject.SetActive(true);
    }

    public void HidePanel() {
        panelObject.SetActive(false);
    }
}
