#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit(0);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        //EditorApplication.Exit(0);    Te cierra Unity xd
#endif
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}