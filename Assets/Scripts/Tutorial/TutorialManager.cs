using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviourSingleton<TutorialManager>
{
    public static bool tutorialWasCompleted;
    [SerializeField] private int currentTutorial;
    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();
    public bool TestInitialTutorial;
    [SerializeField] private GameObject tutorialParent;

    public override void Awake ()
    {
        base.Awake();
        for (int i = 0; i < tutorials.Count; i++)
        {
            tutorials[i].OnTutorialDone += NextTutorial;
        }
    }

    public void StartTutorial (int index, int step)
    {
        if (index == -1 || step == -1)
        {
            gameObject.SetActive(false);
            return;
        }

        tutorials[index].gameObject.SetActive(true);
        tutorials[index].EnableStep(step);
    }

    private void NextTutorial ()
    {
        tutorials[currentTutorial].gameObject.SetActive(false);
        currentTutorial++;
        if (currentTutorial >= tutorials.Count)
        {
            GameManager.Get().SetTutorialDone();
        }
        else
        {
            Invoke(nameof(DelayStartNextTutorial), 1f);
        }
    }

    void DelayStartNextTutorial ()
    {
        tutorials[currentTutorial].gameObject.SetActive(true);
        tutorials[currentTutorial].EnableStep(0);
    }
}