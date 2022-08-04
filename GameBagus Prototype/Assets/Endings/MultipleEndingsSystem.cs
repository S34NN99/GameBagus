using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    public HashSet<string> AttributeList { get; private set; } = new();

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

            showAttributeListDropdown = EditorGUILayout.Foldout(showAttributeListDropdown, "Attribute List");
            if (showAttributeListDropdown) {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;

                int counter = 0;
                foreach (var attribute in multipleEndingsSystem.AttributeList) {
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
