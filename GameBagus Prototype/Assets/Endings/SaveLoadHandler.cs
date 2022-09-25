using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadHandler {
    public static void Save<T>(this T saveData, string file) {
        string filePath = Path.Combine(Application.persistentDataPath, file);
        BinaryFormatter binaryFormatter = new();
        FileStream fileStream = new(filePath, FileMode.Create);

        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();
        Debug.Log("File saved");
    }

    public static (T saveData, bool success) Load<T>(string file) {
        string filePath = Path.Combine(Application.persistentDataPath, file);
        (T saveData, bool success) result = (default(T), false);

        if (File.Exists(filePath)) {
            BinaryFormatter binaryFormatter = new();
            FileStream fileStream = new(filePath, FileMode.Open);

            if (binaryFormatter.Deserialize(fileStream) is T save) {
                Debug.Log("File loaded");
                result = (save, true);
            } else {
                Debug.Log("Incompatible file found, default values will be loaded");
            }

            fileStream.Close();
        } else {
            Debug.Log("File not found, default values will be loaded");
        }

        return result;
    }

    public static void Delete(string file) {
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if (File.Exists(filePath)) {
            File.Delete(filePath);
            Debug.Log("File deleted");
        } else
            Debug.Log("File not found");
    }
}
