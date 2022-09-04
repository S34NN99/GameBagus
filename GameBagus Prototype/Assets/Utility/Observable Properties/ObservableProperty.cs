using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Events;

public abstract class ObservableVariable : MonoBehaviour {
    #region Static Functions
    /// <summary>
    /// Finds the <see cref="ObservableVariable"/> in <i><paramref name="parameters"/></i> that has a matching id with <i><paramref name="uniqueId"/></i>
    /// </summary>
    /// <param name="parameters">List of <see cref="ObservableVariable"/> to search</param>
    /// <param name="uniqueId">Finds the <see cref="ObservableVariable"/> that has this id</param>
    /// <returns></returns>
    public static ObservableVariable FindProperty(IEnumerable<ObservableVariable> parameters, string uniqueId) {
        return parameters.FirstOrDefault(_ => _.UniqueId == uniqueId);
    }

    /// <summary>
    /// Finds the <see cref="ObservableVariable"/> in the scene that has a matching id with <i><paramref name="uniqueId"/></i>
    /// </summary> 
    /// <param name="uniqueId">Finds the <see cref="ObservableVariable"/> that has this id</param>
    /// <returns></returns>
    public static ObservableVariable FindProperty(string uniqueId) => FindProperty(FindObjectsOfType<ObservableVariable>(), uniqueId);

    /// <summary>
    /// Finds the <see cref="ObservableVariable"/> in <i><paramref name="parameters"/></i> that has a matching id with <i><paramref name="uniqueId"/></i>
    /// </summary>
    /// <param name="parameters">List of <see cref="ObservableVariable"/> to search</param>
    /// <param name="uniqueId">Finds the <see cref="ObservableVariable"/> that has this id</param>
    /// <returns></returns>
    public static T FindProperty<T>(IEnumerable<T> parameters, string uniqueId) where T : ObservableVariable {
        return parameters.FirstOrDefault(_ => _.UniqueId == uniqueId);
    }

    /// <summary>
    /// Finds the <see cref="ObservableVariable"/> in the scene that has a matching id with <i><paramref name="uniqueId"/></i>
    /// </summary> 
    /// <param name="uniqueId">Finds the <see cref="ObservableVariable"/> that has this id</param>
    /// <returns></returns>
    public static T FindProperty<T>(string uniqueId) where T : ObservableVariable => FindProperty(FindObjectsOfType<T>(), uniqueId);

    /// <summary>
    /// <para>Substrings enclosed with curly brackets are seen as runtime variables.</para>
    /// <para>Finds <see cref="ObservableVariable"/> with an id that matches the enclosed substring.</para>
    /// <para>Replaces the enclosed substring with the value from <see cref="GetRuntimeValueAsText"/></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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
            ObservableVariable parameter = FindProperty(match.Value.Trim('{', '}'));
            if (parameter != null) {
                outputText += parameter.GetRuntimeValueAsText();
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


    #endregion

    [Tooltip("A string as its unique identifier.")]
    [SerializeField] protected string _uniqueId;
    /// <summary>
    /// A string as its unique identifier.
    /// </summary>
    public string UniqueId => _uniqueId;

    public abstract string GetRuntimeValueAsText();
}

public abstract class ObservableProperty<T> : ObservableVariable {
    [SerializeField] protected T _value;
    public virtual T Value {
        get => _value;
        set {
            OnValueUpdated.Invoke(_value, value);
            _value = value;
            UpdateUiCallback.Invoke(_value);
        }

    }

    [SerializeField] private UnityEvent<T> _updateUiCallback;
    public UnityEvent<T> UpdateUiCallback => _updateUiCallback;

    [SerializeField] private UnityEvent<T, T> _onValueUpdated;
    /// <summary>
    /// <para>Invoked when the property's value is changed. It is not fired when it is updated to the same value.</para>
    /// <para>•    float -> Old value</para>
    /// <para>•    float -> New value</para>
    /// </summary>
    public UnityEvent<T, T> OnValueUpdated => _onValueUpdated;

    public override string GetRuntimeValueAsText() => Value.ToString();
}

public abstract class ObservableEquatableProperty<T> : ObservableProperty<T> where T : IEquatable<T> {
    public override T Value {
        get => _value;
        set {
            if (!_value.Equals(value)) {
                base.Value = value;
            }
        }

    }

}

public abstract class CollectionProperty<T, U> : ObservableProperty<T> where T : ICollection<U> {
    public virtual void Add(U element) {
        Value.Add(element);
        OnValueUpdated.Invoke(Value, Value);
    }

    public void Clear() {
        Value.Clear();
        OnValueUpdated.Invoke(Value, Value);
    }

    public virtual bool Remove(U element) {
        if (Value.Remove(element)) {
            OnValueUpdated.Invoke(Value, Value);
            return true;
        }
        return false;
    }
}
