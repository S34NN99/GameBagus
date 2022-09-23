using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class GO_Utility : MonoBehaviour {
    private Queue<System.Action> queue;

    private bool isExecuting;
    private Coroutine delayCoroutine;

    private void Awake() {
        queue = new();
    }

    private void Update() {
        if (isExecuting) {
            while (queue.Any()) {
                queue.Dequeue().Invoke();
            }
        }
    }

    public void Delay(float delay) {
        if (!isExecuting) {
            StopCoroutine(delayCoroutine);
        }
        delayCoroutine = StartCoroutine(DelayCoroutine());

        IEnumerator DelayCoroutine() {
            isExecuting = false;
            yield return new WaitForSeconds(delay);
            isExecuting = true;
        }
    }

    public void Destroy(GameObject target) {
        queue.Enqueue(() => Destroy(target));
    }

    public void ClickButton(Button button) {
        queue.Enqueue(button.onClick.Invoke);
    }
}
