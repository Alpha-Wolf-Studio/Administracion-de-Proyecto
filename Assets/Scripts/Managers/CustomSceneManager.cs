using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{

    [Header("Fade Configurations")]
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
        yield return StartCoroutine(FadeIn());
        asyncOperation.allowSceneActivation = true;
        yield return StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        fadeImageCanvasGroup.blocksRaycasts = true;
        float t = 0;
        while (t < 1)
        {
            fadeImageCanvasGroup.alpha = t;
            t += Time.deltaTime * fadeSpeed;
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
            t -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

}