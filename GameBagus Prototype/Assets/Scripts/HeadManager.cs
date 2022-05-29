using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HeadManager : MonoBehaviour {
    private static HeadManager _instance;
    public static HeadManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameObject("Head Manager").AddComponent<HeadManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private float posThreshold = 5f;
    [SerializeField] private float posSmoothSpeed = 10f;
    [SerializeField] private float dropSpeed = 15f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private RectTransform dropAnchor;

    private int headCount;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(this);
        }

    }

    public void RollHead(GameObject head) {
        head.transform.SetParent(transform);

        headCount++;
        StartCoroutine(RollHeadCoroutine());

        IEnumerator RollHeadCoroutine() {
            RectTransform headTransform = head.GetComponent<RectTransform>();
            headTransform.anchorMin = new Vector2(0.5f, 0.5f);
            headTransform.anchorMax = new Vector2(0.5f, 0.5f);
            headTransform.pivot = new Vector2(0.5f, 0.5f);

            Vector2 posSmoothVelocity = new Vector2(0, 0);

            while ((headTransform.anchoredPosition - dropAnchor.anchoredPosition).magnitude > posThreshold) {
                Vector2 currentPos = Vector2.SmoothDamp(headTransform.anchoredPosition, dropAnchor.anchoredPosition, ref posSmoothVelocity, posSmoothSpeed);
                headTransform.anchoredPosition = currentPos;

                Quaternion currentRot = headTransform.rotation;
                Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
                headTransform.rotation = rotSpeed * currentRot;

                yield return new WaitForEndOfFrame();
            }

            while (headTransform.anchoredPosition.y > -1200) {
                Vector2 currentPos = headTransform.anchoredPosition;
                currentPos.y -= dropSpeed * Time.deltaTime;
                headTransform.anchoredPosition = currentPos;

                Quaternion currentRot = headTransform.rotation;
                Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
                headTransform.rotation = rotSpeed * currentRot;

                yield return new WaitForEndOfFrame();
            }


        }
    }
}
