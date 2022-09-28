
using UnityEngine;

public class StringContent : MonoBehaviour {
    [SerializeField] [TextArea(4, 8)] private string _content;
    public string Content => _content;
}
