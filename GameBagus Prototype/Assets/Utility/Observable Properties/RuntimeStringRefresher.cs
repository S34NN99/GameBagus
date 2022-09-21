using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using TMPro;
#endif

public class RuntimeStringRefresher : MonoBehaviour {
    [SerializeField] private bool refreshOnUpdate;

    [SerializeField] [RuntimeString(4)] private string _text;
    public string Text { get => _text; set => _text = value; }

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
}