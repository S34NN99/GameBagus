using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HeadManager : MonoBehaviour {
    [SerializeField] private float decapitationForce = 200f;
    [SerializeField] private float sideForce = 1f;
    [SerializeField] private GameObject colliderTemplate;

    [SerializeField] private IntProperty headCountProp;

    public void RollHead(GameObject candleBody) {
        GameObject attachedCollider = Instantiate(colliderTemplate, transform);
        GameObject clonedCandleBody = Instantiate(candleBody);

        attachedCollider.transform.position = candleBody.transform.position;
        clonedCandleBody.transform.SetParent(attachedCollider.transform);
        clonedCandleBody.transform.localPosition = Vector3.zero;

        Rigidbody2D rb2D = attachedCollider.GetComponent<Rigidbody2D>();
        BoxCollider2D boxCollider = attachedCollider.GetComponent<BoxCollider2D>();

        rb2D.AddForceAtPosition(new Vector2(decapitationForce, 0), new Vector3(0, boxCollider.offset.y + boxCollider.size.y / 2));

        StartCoroutine(distributeForce());
        headCountProp.Value++;


        IEnumerator distributeForce() {
            float timer = 0;
            while (true) {
                timer += Time.deltaTime;
                if (timer >= 2f) {
                    break;
                }
                yield return new WaitForEndOfFrame();
                rb2D.AddForce(new Vector2(sideForce, 0));
            }
        }
    }
}
