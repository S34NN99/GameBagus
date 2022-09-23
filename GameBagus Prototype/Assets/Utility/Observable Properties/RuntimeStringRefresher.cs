using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class RuntimeStringRefresher : MonoBehaviour {
    [SerializeField] private bool refreshOnUpdate;

    [SerializeField] [RuntimeString(4)] private string _text;
    public string Text { get => _text; set => _text = value; }

    public StringProperty TextProp { set => Text = value.Value; }
    public RuntimeStringRefresher RuntimeString { set => Text = value.Text; }
    public TextMeshProUGUI TMProUGUI { set => Text = value.text; }

    [SerializeField] private UnityEvent<string> updateTextCallback;

#if UNITY_EDITOR
    private void OnValidate() {
        if (gameObject.TryGetComponent(out TextMeshProUGUI textMeshProUGUI)) {
            textMeshProUGUI.text = Text;
        }
    }
#endif

    private void Update() {
        if (refreshOnUpdate) {
            RefreshText();
        }
    }

    public void RefreshText() {
        string runtimeText = ObservableVariable.ConvertToRuntimeText(Text);
        updateTextCallback.Invoke(runtimeText);
    }

    public void GetText(string text) {
        Text = text;
    }
}