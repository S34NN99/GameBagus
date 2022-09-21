using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GO_Utility : MonoBehaviour {
    [SerializeField] private float delay;
    public void Destroy(GameObject target) {
        Destroy(target, delay);
    }

    public void ClickButton(Button button) {
        button.onClick.Invoke();
    }
}
