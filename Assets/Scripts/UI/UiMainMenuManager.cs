using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UiMainMenuManager : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private CanvasGroup[] menues;

    enum Menu
    {
        Main,
        Settings,
        Credits
    }
    private Menu menu = Menu.Main;

    private void Awake()
    {
        foreach (CanvasGroup menu in menues)
        {
            menu.blocksRaycasts = false;
            menu.interactable = false;
            menu.alpha = 0;
        }

        menues[(int) Menu.Main].interactable = true;
        menues[(int) Menu.Main].blocksRaycasts = true;
        menues[(int) Menu.Main].alpha = 1;
    }
    private void Start()
    {
        Time.timeScale = 1;
        AudioManager.Get().PlayMenuMusic();
    }
    public void OnButtonPlay()
    {
        CustomSceneManager.Get().LoadScene("GamePlay");
    }
    public void OnButtonCreateUnits()
    {
        CustomSceneManager.Get().LoadScene("UnitsCreator");
    }
    public void OnButtonSetting()
    {
        StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Settings, (int) Menu.Main));
    }
    public void OnButtonBackSettings()
    {
        StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main, (int)Menu.Settings));
    }
    public void OnButtonCredits()
    {
        StartCoroutine(SwitchPanel(transitionTime, (int)Menu.Credits, (int)Menu.Main));
    }
    public void OnButtonBackCredits()
    {
        StartCoroutine(SwitchPanel(transitionTime, (int)Menu.Main, (int)Menu.Credits));
    }
    public void OnButtonExit()
    {
        Application.Quit(0);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
    IEnumerator SwitchPanel(float maxTime, int onMenu, int offMenu)
    {
        float onTime = 0;
        CanvasGroup on = menues[onMenu];
        CanvasGroup off = menues[offMenu];

        off.blocksRaycasts = false;
        off.interactable = false;

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float fade = onTime / maxTime;
            on.alpha = fade;
            off.alpha = 1 - fade;
            yield return null;
        }
        on.blocksRaycasts = true;
        on.interactable = true;
        onTime = 0;

        menu = (Menu)onMenu;
    }
}