using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviourSingleton<TutorialManager>
{
    public static bool tutorialWasCompleted;
    [SerializeField] private int currentTutorial;
    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();
    public bool TestInitialTutorial;

    private void Start ()
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            tutorials[i].OnTutorialDone += NextTutorial;
        }
    }

    void Update ()
    {
        if (TestInitialTutorial)
        {
            TestInitialTutorial = false;
            tutorials[0].gameObject.SetActive(true);
            tutorials[0].EnableStep();
        }
    }

    private void NextTutorial ()
    {
        tutorials[currentTutorial].gameObject.SetActive(false);
        currentTutorial++;
        if (currentTutorial >= tutorials.Count)
            tutorialWasCompleted = true;
        else
        {
            tutorials[currentTutorial].gameObject.SetActive(true);
            tutorials[currentTutorial].EnableStep();
        }
    }
}