using System.IO;
using UnityEngine;
//using Newtonsoft.Json;

public class LoadAndSave
{

    public static string LoadFromFile(string filename, bool playerPref = false, bool resourcesLoad = true)
    {
        if (playerPref)
        {
            if (PlayerPrefs.HasKey(filename))
            {
                return PlayerPrefs.GetString(filename);
            }
            
            return null;
        }
        string stringData = null;

        if (resourcesLoad)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(filename);
            if (textAsset == null) return null;
            stringData = textAsset.text;
        }
        else
        {
            string path = Application.dataPath + "/Resources/" + filename + ".txt";
            if(File.Exists(path))
                stringData = File.ReadAllText(path);
            
        }
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
#endif
        }
    }
}