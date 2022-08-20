#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;
using System.Collections.Generic;

public class StringListToCSV : EditorWindow {
    //string myString = "Hello World";
    //bool groupEnabled;
    //bool myBool = true;
    //float myFloat = 1.23f; 

    private string separatorText;
    private List<string> strList;
    private string outputCSVText;

    private ReorderableList strReorderableList;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Custom/String List To CSV Converter")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        StringListToCSV window = (StringListToCSV)GetWindow(typeof(StringListToCSV));
        window.Show();
    }

    private void OnEnable() {
        separatorText = ", ";
        outputCSVText = "";
        strList = new();
        strReorderableList = new ReorderableList(strList, typeof(string));

        strReorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
            strList[index] = EditorGUI.TextField(rect, strList[index]);
        };
        strReorderableList.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, "Input Strings");
        };
    }

    private void OnGUI() {

        EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
        separatorText = EditorGUILayout.TextField("Separator text", separatorText);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Inputs", EditorStyles.boldLabel);
        strReorderableList.DoLayoutList();

        EditorGUILayout.Space();

        outputCSVText = string.Join(separatorText, strList);
        EditorGUILayout.LabelField("Output CSV Text", EditorStyles.boldLabel);
        EditorGUILayout.TextArea(outputCSVText);
        //myString = EditorGUILayout.TextField("Text Field", myString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();  
    }
}


//public class GameEventEditorWindow : EditorWindow {
//    private IReadOnlyList<ProjectEvent> projectEvents;
//    private IReadOnlyList<ManagementEvent> managementEvents;
//    private IReadOnlyList<StoryEvent> storyEvents;


//    // Add menu named "My Window" to the Window menu
//    [MenuItem("Window/Custom/Game Events")]
//    static void Init() {
//        // Get existing open window or if none, make a new one:
//        GameEventEditorWindow window = (GameEventEditorWindow)GetWindow(typeof(GameEventEditorWindow));
//        window.Show();
//    }

//    private void OnEnable() {
//        projectEvents = FindObjectsOfType<ProjectEvent>();
//        managementEvents = FindObjectsOfType<ManagementEvent>();
//        storyEvents = FindObjectsOfType<StoryEvent>();


//    }

//    private void OnGUI() {


//    }
//}
#endif