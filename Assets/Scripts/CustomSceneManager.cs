using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{    
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}