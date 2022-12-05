using System.IO;
using UnityEngine;
//using Newtonsoft.Json;

public class LoadAndSave
{

    public static string LoadFromFile(string filename, bool playerPref = false)
    {
        if (playerPref)
        {
            if (PlayerPrefs.HasKey(filename))
            {
                return PlayerPrefs.GetString(filename);
            }
            
            return null;
        }
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        if (textAsset == null) return null;
        string stringData = textAsset.text;
        return stringData;
    }

    public static void SaveToFile(string filename, string stringData, bool playerPref = false)
    {
        if (playerPref)
        {
            PlayerPrefs.SetString(filename, stringData);
            PlayerPrefs.Save();
        }
        else
        {
            string path = Application.dataPath + "/Resources/" + filename + ".txt";
            File.WriteAllText(path, stringData);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#else
                File.WriteAllText(filename + ".txt", stringData);
#endif
        }
    }

    public static void OverwriteResourceFile(string filename, bool playerPref = false)
    {
        string path = filename + ".txt";
        string data = File.ReadAllText(path);

        if (playerPref)
        {
            PlayerPrefs.SetString(filename, data);
            PlayerPrefs.Save();
        }
        else
        {
            SaveToFile(filename, data, true);
        }
    }
}