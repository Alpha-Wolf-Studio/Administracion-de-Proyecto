using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tutorial : MonoBehaviour
{
    public event System.Action OnTutorialDone;
    public int currentStep;
    public List<TutorialStep> steps = new List<TutorialStep>();
    
    private void Awake ()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].OnStepDone += NextStep;
        }
    }

    private void Start ()
    {
        EnableStep(0);
    }

    public void EnableStep (int loadStep)
    {
        currentStep = loadStep;
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].gameObject.SetActive(false);
        }

        steps[currentStep].gameObject.SetActive(true);

        steps[currentStep].otterAnimation.SetAnimation(currentStep == 0 ? TutoAnimTypeOtter.Spawn : TutoAnimTypeOtter.Speak);
    }

    private void NextStep ()
    {
        steps[currentStep].gameObject.SetActive(false);
        currentStep++;

        if (currentStep >= steps.Count)
            OnTutorialDone?.Invoke();
        else
            steps[currentStep].gameObject.SetActive(true);
    }
}