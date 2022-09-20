using UnityEngine;
using UnityEngine.Events;

public class RuntimeStringRefresher : MonoBehaviour {
    [SerializeField] private bool refreshOnUpdate;

    [SerializeField] [RuntimeString(4)] private string _text;
    public string Text { get => _text; set => _text = value; }

    [SerializeField] private UnityEvent<string> updateTextCallback;

    private void Update() {
        if (refreshOnUpdate) {
            RefreshText();
        }
    }

    public void RefreshText() {
        string runtimeText = ObservableVariable.ConvertToRuntimeText(Text);
        updateTextCallback.Invoke(runtimeText);
    }
}