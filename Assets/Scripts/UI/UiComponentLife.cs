using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiComponentLife : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image imageFill;

    private CanvasGroup canvasGroup;

    private float showingTime = 5f;
    private float transparencyTime = 1f;
    private IEnumerator HideProcessCor;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        unit.OnTakeDamage += TakeDamageUI;
    }

    private void Update()
    {
        transform.forward = Vector3.forward;
    }

    void TakeDamageUI(float currentLife, float maxLife)
    {
        ShowPanel();
        imageFill.fillAmount = currentLife / maxLife;
    }

    void ShowPanel()
    {
        canvasGroup.alpha = 1;

        if (HideProcessCor != null)
            StopCoroutine(HideProcessCor);

        HideProcessCor = HideProcess();
        StartCoroutine(HideProcessCor);
    }

    IEnumerator HideProcess()
    {
        yield return new WaitForSeconds(showingTime);
        float onTime = 0;
        while (onTime < transparencyTime)
        {
            onTime += Time.deltaTime;
            canvasGroup.alpha = 1 - onTime / transparencyTime;
            yield return null;
        }
    }
}