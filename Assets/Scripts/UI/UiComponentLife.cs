using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiComponentLife : MonoBehaviour
{
    [SerializeField] private Unit unit = default;
    [SerializeField] private Image imageFill = default;
    [SerializeField] private bool isAlwaysShown = false;

    private CanvasGroup canvasGroup = default;

    private float showingTime = 5f;
    private float transparencyTime = 1f;
    private IEnumerator HideProcessIEnumerator = null;

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
        if (isAlwaysShown) canvasGroup.alpha = 1f; 
        else ShowPanelAnimation();
        imageFill.fillAmount = currentLife / maxLife;
    }

    void ShowPanelAnimation()
    {
        canvasGroup.alpha = 1;

        if (HideProcessIEnumerator != null)
            StopCoroutine(HideProcessIEnumerator);

        HideProcessIEnumerator = HideProcess();
        StartCoroutine(HideProcessIEnumerator);
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