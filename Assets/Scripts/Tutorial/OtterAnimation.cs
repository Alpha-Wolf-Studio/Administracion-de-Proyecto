using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtterAnimation : MonoBehaviour
{
    public Action OnEndWriting;
    private static readonly int AnimTypeOtter = Animator.StringToHash("TutoAnimTypeOtter");
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text textTutorial;
    [SerializeField] private float maxTimeTextCompleted = 5f;
    [SerializeField] private string textInBoxTutorial;

    private TutoAnimTypeOtter tutoAnimTypeOtter = TutoAnimTypeOtter.Spawn;
    private IEnumerator coroutineWritingText;
    private float timeTextCompleted;

    [SerializeField] private float maxCountTimeIdle = 5.0f;
    private float countTimeIdle;
    [SerializeField] private float maxCountTimeAngry= 3.0f;
    private float countTimeAngry;

    private void OnEnable ()
    {
        timeTextCompleted = maxTimeTextCompleted;
        SetCoroutineWritingText(true);
    }

    private void Update ()
    {
        switch (tutoAnimTypeOtter)
        {
            case TutoAnimTypeOtter.Spawn:
                break;
            case TutoAnimTypeOtter.Idle:
                countTimeIdle += Time.deltaTime;
                if (countTimeIdle > maxCountTimeIdle)
                {
                    SetAnimation(TutoAnimTypeOtter.Angry);
                    countTimeIdle = 0;
                }
                break;
            case TutoAnimTypeOtter.Speak:
                break;
            case TutoAnimTypeOtter.Angry:
                countTimeAngry += Time.deltaTime;
                if (countTimeAngry > maxCountTimeAngry)
                {
                    SetAnimation(TutoAnimTypeOtter.Idle);
                    countTimeAngry = 0;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetAnimation (TutoAnimTypeOtter animTypeOtter)
    {
        tutoAnimTypeOtter = animTypeOtter;
        animator.SetInteger(AnimTypeOtter, (int) tutoAnimTypeOtter);
    }

    private void SetCoroutineWritingText (bool isEnable)
    {
        if (coroutineWritingText != null)
            StopCoroutine(coroutineWritingText);

        if (isEnable)
        {
            coroutineWritingText = WritingText();
            StartCoroutine(coroutineWritingText);
        }
    }

    private IEnumerator WritingText ()
    {
        yield return new WaitForSeconds(0.5f);
        int maxLenghtString = textInBoxTutorial.Length;
        SetAnimation(TutoAnimTypeOtter.Speak);

        while (timeTextCompleted > 0)
        {
            timeTextCompleted -= Time.deltaTime;
            float lerp = (maxTimeTextCompleted - timeTextCompleted) * maxLenghtString/ maxTimeTextCompleted;
            textTutorial.text = textInBoxTutorial.Substring(0, (int) lerp);
            yield return null;
        }

        SetAnimation(TutoAnimTypeOtter.Idle);
        textTutorial.text = textInBoxTutorial;
        OnEndWriting?.Invoke();
    }

    public void EndWriting ()
    {
        timeTextCompleted = 0;
    }
}

public enum TutoAnimTypeOtter
{
    Spawn,
    Idle,
    Speak,
    Angry
}