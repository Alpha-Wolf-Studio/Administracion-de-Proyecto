using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    [Header("Fade Configurations")] 
    private static readonly int IsOpen = Animator.StringToHash("IsClose");
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject ourCanvas;
    [SerializeField] private CanvasGroup fadeImageCanvasGroup = default;
    [SerializeField] private float fadeSpeed = 1f;

    public override void Awake()
    {
        base.Awake();
        fadeImageCanvasGroup.gameObject.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
        ourCanvas.SetActive(true);
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    private IEnumerator LoadSceneCoroutine(string scene)
    {
        Time.timeScale = 1;
        var asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        animator.SetBool(IsOpen, true);
        yield return StartCoroutine(FadeIn());
        asyncOperation.allowSceneActivation = true;
        animator.SetBool(IsOpen, false);
        yield return StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        fadeImageCanvasGroup.blocksRaycasts = true;
        float t = 0;
        while (t < 1)
        {
            fadeImageCanvasGroup.alpha = t;
            t += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }
    }
    
    private IEnumerator FadeOut()
    {
        fadeImageCanvasGroup.blocksRaycasts = false;
        float t = 1;
        while (t > 0)
        {
            fadeImageCanvasGroup.alpha = t;
            t -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }
    }
}