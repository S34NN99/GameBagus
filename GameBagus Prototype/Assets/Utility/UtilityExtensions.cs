
using UnityEngine;
using System.Collections.Generic;

using System.Linq;

public static class UtilityExtensions {
    public static string Random(this IReadOnlyList<string> targetList) {
        if (!targetList.Any()) {
            return "";
        }
        return targetList[UnityEngine.Random.Range(0, targetList.Count)];
    }
}