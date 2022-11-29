using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtterAnimation : MonoBehaviour
{
    private static readonly int AnimTypeOtter = Animator.StringToHash("TutoAnimTypeOtter");
    [SerializeField] private Animator animator;
    private TutoAnimTypeOtter tutoAnimTypeOtter = TutoAnimTypeOtter.Spawn;
    [SerializeField] private float timeTextCompleted = 1f;
    [SerializeField] private string textInBoxTutorial;
    private TMP_Text textTutorial;
    private IEnumerator coroutineWritingText;
    

    private void OnEnable ()
    {
        SetAnimation(TutoAnimTypeOtter.Spawn);

        SetCoroutineWritingText(true);
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
        int maxLenghtString = textInBoxTutorial.Length;
        SetAnimation(TutoAnimTypeOtter.Speak);

        while (timeTextCompleted > 0)
        {
            timeTextCompleted -= Time.deltaTime;
            float lerp = (1 - timeTextCompleted) * maxLenghtString;
            textTutorial.text = textInBoxTutorial.Substring(0, (int) lerp);
        }

        SetAnimation(TutoAnimTypeOtter.Idle);
        textTutorial.text = textInBoxTutorial;
        yield return null;
    }
}

public enum TutoAnimTypeOtter
{
    Spawn,
    Idle,
    Speak,
    Angry
}