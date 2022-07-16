using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Events;

public abstract class ObservableParameter : MonoBehaviour {
    public static ObservableParameter FindProperty(IEnumerable<ObservableParameter> parameters, string uniqueId) {
        return parameters.FirstOrDefault(_ => _.UniqueId == uniqueId);
    }

    public static ObservableParameter FindProperty(string uniqueId) => FindProperty(FindObjectsOfType<ObservableParameter>(), uniqueId);

    public static T FindProperty<T>(IEnumerable<T> parameters, string uniqueId) where T : ObservableParameter {
        return parameters.FirstOrDefault(_ => _.UniqueId == uniqueId);
    }

    public static T FindProperty<T>(string uniqueId) where T : ObservableParameter => FindProperty(FindObjectsOfType<T>(), uniqueId);

    public static string ConvertToRuntimeText(string text) {
        if (text == null || text == "") return "";

        Regex regex = new("{.*?}");
        MatchCollection matches = regex.Matches(text);

        string outputText = "";
        int previousIndex = 0;
        foreach (Match match in matches) {
            if (previousIndex < match.Index) {
                // add text before the match
                outputText += text.Substring(previousIndex, match.Index - previousIndex);
            }

            // add match
            ObservableParameter parameter = FindProperty(match.Value.Trim('{', '}'));
            if (parameter != null) {
                outputText += parameter.GetValueAsText();
            } else {
                outputText += "unspecified value";
            }

            previousIndex = match.Index + match.Length;
        }

        if (previousIndex != text.Length) {
            outputText += text.Substring(previousIndex);
        }

        return outputText;
    }

    [Tooltip("A string as its unique identifier.")]
    [SerializeField] protected string _uniqueId;
    /// <summary>
    /// A string as its unique identifier.
    /// </summary>
    public string UniqueId => _uniqueId;

    public abstract string GetValueAsText();
}

public abstract class ObservableProperty<T> : ObservableParameter {
    [SerializeField] protected T _value;
    public virtual T Value {
        get => _value;
        set {
            OnValueUpdated.Invoke(_value, value);
            _value = value;
        }

    }


    [SerializeField] private UnityEvent<T, T> _onValueUpdated;
    /// <summary>
    /// <para>Invoked when the property's value is changed. It is not fired when it is updated to the same value.</para>
    /// <para>•    float -> Old value</para>
    /// <para>•    float -> New value</para>
    /// </summary>
    public UnityEvent<T, T> OnValueUpdated => _onValueUpdated;
}
