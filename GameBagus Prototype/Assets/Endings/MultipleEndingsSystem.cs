using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    [SerializeField] private HashSetStringProperty _attributeList;
    public HashSetStringProperty AttributeList => _attributeList;

    [SerializeField] private MetricsPanel metricsPanel;

    [SerializeField] private StoryCheckpoint[] _endings;
    public StoryCheckpoint[] Endings => _endings;

    private void Awake() {
        AttributeList.Value = new HashSet<string>();
    }

    public void AddAttribute(string attribute) {
        AttributeList.Add(attribute);
    }

    public void RemoveAttribute(string attribute) {
        AttributeList.Remove(attribute);
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(MultipleEndingsSystem))]
    private class MultipleEndingsSystemEditor : Editor {
        private MultipleEndingsSystem multipleEndingsSystem;

        private bool showAttributeListDropdown;

        private void OnEnable() {
            multipleEndingsSystem = target as MultipleEndingsSystem;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (multipleEndingsSystem.AttributeList.Value == null) return;
            showAttributeListDropdown = EditorGUILayout.Foldout(showAttributeListDropdown, "Attribute List");
            if (showAttributeListDropdown) {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;

                int counter = 0;
                foreach (var attribute in multipleEndingsSystem.AttributeList.Value) {
                    EditorGUILayout.TextField("Element " + counter, attribute);
                    counter++;
                }

                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

        }
    }
#endif
}
