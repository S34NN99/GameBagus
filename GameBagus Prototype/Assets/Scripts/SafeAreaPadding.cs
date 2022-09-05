using System.Collections;
using UnityEngine;

namespace Ludus.UI
{
    public class SafeAreaPadding : MonoBehaviour
    {
        private enum Delay {None,OneFrame}
        [SerializeField] private Delay delay = Delay.OneFrame;
        [SerializeField] private RectTransform mainRect = null;

        private IEnumerator Start()
        {
            if (!mainRect) mainRect = GetComponent<RectTransform>();
            if (delay == Delay.OneFrame) yield return new WaitForEndOfFrame();
            SetAnchor();
        }

        /// <summary>
        /// Sets the anchor based on safe Area
        /// </summary>
        void SetAnchor()
        {
            mainRect.anchorMin = new Vector2(Screen.safeArea.xMin / (float)Screen.width, Screen.safeArea.yMin / (float)Screen.height);
            mainRect.anchorMax = new Vector2(Screen.safeArea.xMax / (float)Screen.width, Screen.safeArea.yMax / (float)Screen.height);
        }


    }
}