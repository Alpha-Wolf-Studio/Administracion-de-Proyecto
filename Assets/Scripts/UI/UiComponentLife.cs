using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiComponentLife : MonoBehaviour
{
    [SerializeField] private Unit unit = default;
    [SerializeField] private Image imageFill = default;
    [SerializeField] private bool isAlwaysShown = false;
    [SerializeField] private Gradient lifeColors = default;

    private CanvasGroup canvasGroup = default;

    private float showingTime = 5f;
    private float transparencyTime = 1f;
    private IEnumerator HideProcessIEnumerator = null;

    private IEnumerator lifeBarReceiveDamageAnimationIEnumerator = null;
    private float lastLifeFill = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        unit.OnDamageRedirect += delegate
        {
            StopAllCoroutines();
            canvasGroup.alpha = 0;
        };
    }

    private void Start()
    {
        unit.OnTakeDamage += TakeDamageUI;
        imageFill.fillAmount = unit.stats.life / unit.initialStats.life;
        imageFill.color = lifeColors.Evaluate(imageFill.fillAmount);
    }

    private void Update()
    {
        transform.forward = Vector3.forward;
    }

    void TakeDamageUI(float currentLife, float maxLife)
    {
        if (isAlwaysShown) canvasGroup.alpha = 1f; 
        else ShowPanelAnimation();

        if (currentLife < 0) 
            currentLife = 0;

        if (lifeBarReceiveDamageAnimationIEnumerator != null)
        {
            imageFill.fillAmount = lastLifeFill;
            StopCoroutine(lifeBarReceiveDamageAnimationIEnumerator);
        }
        lifeBarReceiveDamageAnimationIEnumerator = lifeBarRecieveDamageAnimation(currentLife, maxLife);
        StartCoroutine(lifeBarReceiveDamageAnimationIEnumerator);
    }

    void ShowPanelAnimation()
    {
        canvasGroup.alpha = 1;

        if (HideProcessIEnumerator != null)
            StopCoroutine(HideProcessIEnumerator);

        HideProcessIEnumerator = HideProcess();
        StartCoroutine(HideProcessIEnumerator);
    }

    private IEnumerator lifeBarRecieveDamageAnimation(float currentLife, float maxLife) 
    {
        float t = imageFill.fillAmount;
        float fillAmount = currentLife / maxLife;
        lastLifeFill = fillAmount;
        while (t > fillAmount) 
        {
            t -= Time.deltaTime;
            imageFill.fillAmount = t;
            imageFill.color = lifeColors.Evaluate(t);
            yield return null;
        }
        imageFill.fillAmount = fillAmount;
        imageFill.color = lifeColors.Evaluate(fillAmount);
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