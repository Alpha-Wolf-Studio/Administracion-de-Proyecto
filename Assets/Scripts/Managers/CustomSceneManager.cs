using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}