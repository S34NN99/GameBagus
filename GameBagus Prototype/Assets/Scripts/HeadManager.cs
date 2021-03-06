using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HeadManager : MonoBehaviour {
    [SerializeField] private float posThreshold = 5f;
    [SerializeField] private float posSmoothSpeed = 10f;
    [SerializeField] private float dropSpeed = 15f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private RectTransform dropAnchor;

    private int headCount;
    public int HeadCount => headCount;

    public void RollHead(GameObject head) {
        RectTransform clonedHead = Instantiate(head, transform).GetComponent<RectTransform>();

        headCount++;
        StartCoroutine(RollHeadCoroutine());

        IEnumerator RollHeadCoroutine() {
            clonedHead.anchorMin = new Vector2(0.5f, 0.5f);
            clonedHead.anchorMax = new Vector2(0.5f, 0.5f);
            clonedHead.pivot = new Vector2(0.5f, 0.5f);

            Vector2 posSmoothVelocity = new Vector2(0, 0);

            while ((clonedHead.anchoredPosition - dropAnchor.anchoredPosition).magnitude > posThreshold) {
                Vector2 currentPos = Vector2.SmoothDamp(clonedHead.anchoredPosition, dropAnchor.anchoredPosition, ref posSmoothVelocity, posSmoothSpeed);
                clonedHead.anchoredPosition = currentPos;

                Quaternion currentRot = clonedHead.rotation;
                Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
                clonedHead.rotation = rotSpeed * currentRot;

                yield return new WaitForEndOfFrame();
            }

            while (clonedHead.anchoredPosition.y > -1200) {
                Vector2 currentPos = clonedHead.anchoredPosition;
                currentPos.y -= dropSpeed * Time.deltaTime;
                clonedHead.anchoredPosition = currentPos;

                Quaternion currentRot = clonedHead.rotation;
                Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
                clonedHead.rotation = rotSpeed * currentRot;

                yield return new WaitForEndOfFrame();
            }


        }
    }
}
