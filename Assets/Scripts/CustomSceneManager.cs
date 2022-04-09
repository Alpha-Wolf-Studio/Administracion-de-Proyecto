using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    private float fadeSpeed = 1;
    
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}