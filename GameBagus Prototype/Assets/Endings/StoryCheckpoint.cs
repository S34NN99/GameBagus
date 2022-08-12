using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Project/Story Checkpoint")]
public class StoryCheckpoint : ScriptableObject {
    [SerializeField] private StoryCheckpointType _checkpointType;
    public StoryCheckpointType CheckpointType => _checkpointType;

    [SerializeField] private Page[] _storyPages;
    public Page[] StoryPages => _storyPages;

    //[SerializeField] private string[] _checkpointParameters;
    //public string[] CheckpointParameters => _checkpointParameters;

    //[SerializeField] private string _nextProjectName;
    //public string NextProjectName => _nextProjectName;

    //[SerializeField] private string[] _requiredAttributes;
    //public string[] RequiredAttributes => _requiredAttributes;

    //public bool CheckEndingConditions(HashSet<string> attributes) {
    //    foreach (var attribute in RequiredAttributes) {
    //        if (!attributes.Contains(attribute)) {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    [System.Serializable]
    public class Page {
        [SerializeField] private string _mainBodyText;
        public string MainBodyText => _mainBodyText;

        [SerializeField] private Sprite _mainGraphics_Image;
        public Sprite MainGraphics_Image => _mainGraphics_Image;

    }
}

public enum StoryCheckpointType {
    InProjectEvent,
    EndOfProject,
    EndOfGame,
}