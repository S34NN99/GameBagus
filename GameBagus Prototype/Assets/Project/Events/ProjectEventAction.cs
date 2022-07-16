using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;

#endif


public class ProjectEventAction : MonoBehaviour {
    [SerializeField] private string _titleText;
    public string TitleText => _titleText;

    [SerializeField] private string _resultText;
    public string ResultText => _resultText;

    [SerializeField] private string[] _resultAttributeList;
    public IReadOnlyList<string> ResultAttributeList => _resultAttributeList;

    [SerializeField] private UnityEvent onActionSelected;

    public void SelectAction() {
        onActionSelected.Invoke();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ProjectEventAction))]
    private class ProjectEventActionEditor : Editor {
        private ProjectEventAction eventAction;

        private void OnEnable() {
            eventAction = target as ProjectEventAction;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Title text", ObservableParameter.ConvertToRuntimeText(eventAction.TitleText));
            EditorGUILayout.LabelField("Result text", ObservableParameter.ConvertToRuntimeText(eventAction.ResultText));
        }
    }
#endif
}
