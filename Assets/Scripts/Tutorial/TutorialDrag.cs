using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDrag : MonoBehaviour
{
    private Slider slider;
    public event Action onSuccessful;

    private void Awake ()
    {
        slider = GetComponent<Slider>();
    }

    void Start ()
    {
        slider.onValueChanged.AddListener(CheckSuccessfully);
        Time.timeScale = 0.1f;
    }

    public void CheckSuccessfully (float currentValue)
    {
        if (currentValue > 0.9f)
        {
            Time.timeScale = 1;
            onSuccessful?.Invoke();
        }
    }
}