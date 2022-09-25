using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null) {
                    Debug.Log($"Each level should have one {typeof(GameManager)}! Adding new GameObject to attach the manager");
                    GameObject eventManagerObj = new GameObject(typeof(GameManager).ToString());
                    _instance = eventManagerObj.AddComponent<GameManager>();
                    _instance.InitManagers();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private GroupChat _groupChat;
    public GroupChat GroupChat => _groupChat;

    [SerializeField] private CandleManager _candleManager;
    public CandleManager CandleManager => _candleManager;

    [SerializeField] private Project _project;
    public Project Project => _project;

    [SerializeField] private Camera _mainCam;
    public Camera MainCam => _mainCam;


    private void Awake() {
        if (_instance == null) {
            _instance = this;
            InitManagers();
        } else if (this != _instance) {
            Destroy(this);

            return;
        }

    }

    private void InitManagers() {
        if (GroupChat == null) {
            _groupChat = FindObjectOfType<GroupChat>();
        }
        if (CandleManager == null) {
            _candleManager = FindObjectOfType<CandleManager>();
        }
        if (Project == null) {
            _project = FindObjectOfType<Project>();
        }
        if (MainCam == null) {
            _mainCam = Camera.main;
        }
    }
}
