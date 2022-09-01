using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HeadManager : MonoBehaviour {
    [SerializeField] private float candleZPos = -10f;
    [SerializeField] private float decapitationForce = 100f;
    [SerializeField] private GameObject colliderTemplate;


    [SerializeField] private Transform dropAnchor;

    [SerializeField] private IntProperty headCountProp;

    public void RollHead(GameObject head) {
        GameObject clonedHead = Instantiate(head, transform);
        GameObject attachedCollider = Instantiate(colliderTemplate, head.transform);
        Rigidbody2D rb2D = attachedCollider.GetComponent<Rigidbody2D>();
        BoxCollider2D boxCollider = attachedCollider.GetComponent<BoxCollider2D>();

        rb2D.AddForceAtPosition(new Vector2(decapitationForce, 0), head.transform.position + new Vector3(0, boxCollider.offset.y + boxCollider.size.y / 2));

        headCountProp.Value++;
        //StartCoroutine(RollHeadCoroutine());

        //IEnumerator RollHeadCoroutine() {
        //    clonedHead.anchorMin = new Vector2(0.5f, 0.5f);
        //    clonedHead.anchorMax = new Vector2(0.5f, 0.5f);
        //    clonedHead.pivot = new Vector2(0.5f, 0.5f);

        //    Vector2 posSmoothVelocity = new Vector2(0, 0);

        //    while ((clonedHead.anchoredPosition - dropAnchor.anchoredPosition).magnitude > posThreshold) {
        //        Vector2 currentPos = Vector2.SmoothDamp(clonedHead.anchoredPosition, dropAnchor.anchoredPosition, ref posSmoothVelocity, posSmoothSpeed);
        //        clonedHead.anchoredPosition = currentPos;

        //        Quaternion currentRot = clonedHead.rotation;
        //        Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
        //        clonedHead.rotation = rotSpeed * currentRot;

        //        yield return new WaitForEndOfFrame();
        //    }

        //    while (clonedHead.anchoredPosition.y > -1200) {
        //        Vector2 currentPos = clonedHead.anchoredPosition;
        //        currentPos.y -= dropSpeed * Time.deltaTime;
        //        clonedHead.anchoredPosition = currentPos;

        //        Quaternion currentRot = clonedHead.rotation;
        //        Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
        //        clonedHead.rotation = rotSpeed * currentRot;

        //        yield return new WaitForEndOfFrame();
        //    }


        //}
    }
    //[SerializeField] private float posThreshold = 5f;
    //[SerializeField] private float posSmoothSpeed = 1f;
    //[SerializeField] private float dropSpeed = 300f;
    //[SerializeField] private float rotateSpeed = -150f;
    //[SerializeField] private RectTransform dropAnchor;

    //[SerializeField] private IntProperty headCountProp;

    //public void RollHead(GameObject head) {
    //    RectTransform clonedHead = Instantiate(head, transform).GetComponent<RectTransform>();

    //    headCountProp.Value++;
    //    StartCoroutine(RollHeadCoroutine());

    //    IEnumerator RollHeadCoroutine() {
    //        clonedHead.anchorMin = new Vector2(0.5f, 0.5f);
    //        clonedHead.anchorMax = new Vector2(0.5f, 0.5f);
    //        clonedHead.pivot = new Vector2(0.5f, 0.5f);

    //        Vector2 posSmoothVelocity = new Vector2(0, 0);

    //        while ((clonedHead.anchoredPosition - dropAnchor.anchoredPosition).magnitude > posThreshold) {
    //            Vector2 currentPos = Vector2.SmoothDamp(clonedHead.anchoredPosition, dropAnchor.anchoredPosition, ref posSmoothVelocity, posSmoothSpeed);
    //            clonedHead.anchoredPosition = currentPos;

    //            Quaternion currentRot = clonedHead.rotation;
    //            Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
    //            clonedHead.rotation = rotSpeed * currentRot;

    //            yield return new WaitForEndOfFrame();
    //        }

    //        while (clonedHead.anchoredPosition.y > -1200) {
    //            Vector2 currentPos = clonedHead.anchoredPosition;
    //            currentPos.y -= dropSpeed * Time.deltaTime;
    //            clonedHead.anchoredPosition = currentPos;

    //            Quaternion currentRot = clonedHead.rotation;
    //            Quaternion rotSpeed = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
    //            clonedHead.rotation = rotSpeed * currentRot;

    //            yield return new WaitForEndOfFrame();
    //        }


    //    }
    //}
}
