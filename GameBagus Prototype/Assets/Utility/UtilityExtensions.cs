
using UnityEngine;
using System.Collections.Generic;

using System.Linq;

public static class UtilityExtensions {
    public static T Random<T>(this IReadOnlyList<T> targetList, T defaultVal = default) {
        if (!targetList.Any()) {
            return defaultVal;
        }
        return targetList[UnityEngine.Random.Range(0, targetList.Count)];
    }

    public static string Random(this IReadOnlyList<string> targetList) => targetList.Random("");

    public static T Random<T>(this IReadOnlyList<T> targetList, System.Random prng, T defaultVal = default) {
        if (!targetList.Any()) {
            return defaultVal;
        }
        return targetList[prng.Next(targetList.Count)];
    }

    public static string Random(this IReadOnlyList<string> targetList, System.Random prng) => targetList.Random(prng, "");
}