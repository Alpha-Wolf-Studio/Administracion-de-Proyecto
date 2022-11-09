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
        EnableStep();
    }

    public void EnableStep ()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].gameObject.SetActive(false);
        }

        steps[currentStep].gameObject.SetActive(true);
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