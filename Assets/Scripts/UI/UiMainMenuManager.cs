using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class UiMainMenuManager : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private CanvasGroup[] menues;

    [Header("References: ")] 
    [SerializeField] private Button btnToMissions;
    [SerializeField] private Button btnToCreator;
    [SerializeField] private Button btnToSettings;
    [SerializeField] private Button btnToCredits;
    [SerializeField] private Button btnToProfile;
    [SerializeField] private Button btnToBarraks;

    [SerializeField] private Button btnBackSettings;
    [SerializeField] private Button btnBackCredits;
    [SerializeField] private Button btnBackProfile;
    [SerializeField] private Button btnBackBarracks;

    [SerializeField] private Button btnMusicOnOff;
    [SerializeField] private Button btnEffectOnOff;
    [SerializeField] private TextMeshProUGUI textVersion;

    [Header("Others: ")]
    [SerializeField] private Sprite spriteAudioOn;
    [SerializeField] private Sprite spriteAudioOff;

    enum Menu
    {
        Main,
        Settings,
        Credits,
        Profile,
        Barracks
    }
    private Menu currentMenu = Menu.Main;

    private void Awake()
    {
        foreach (CanvasGroup menu in menues)
        {
            menu.blocksRaycasts = false;
            menu.interactable = false;
            menu.alpha = 0;
            menu.gameObject.SetActive(false);
        }

        menues[(int) Menu.Main].gameObject.SetActive(true);
        menues[(int) Menu.Main].interactable = true;
        menues[(int) Menu.Main].blocksRaycasts = true;
        menues[(int) Menu.Main].alpha = 1;
    }

    private void Start()
    {
        Time.timeScale = 1;
        AddLiseners();
        AudioManager.Get().PlayMusicMenu();
        textVersion.text = "Version: " + Application.version;
    }

    private void AddLiseners()
    {
        btnToMissions.onClick.AddListener(OnButtonPlay);
        btnToCreator.onClick.AddListener(OnButtonCreateUnits);
        btnToSettings.onClick.AddListener(OnButtonSetting);
        btnToCredits.onClick.AddListener(OnButtonCredits);
        btnToProfile.onClick.AddListener(OnButtonProfile);
        btnToBarraks.onClick.AddListener(OnButtonBarracks);

        btnBackSettings.onClick.AddListener(OnButtonBackSettings);
        btnBackCredits.onClick.AddListener(OnButtonBackCredits);
        btnBackProfile.onClick.AddListener(OnButtonBackProfile);
        btnBackBarracks.onClick.AddListener(OnButtonBackBarracks);

        btnMusicOnOff.onClick.AddListener(EnableMusic);
        btnEffectOnOff.onClick.AddListener(EnableEffect);
    }

    private void OnButtonPlay() => CustomSceneManager.Get().LoadScene("GamePlay");
    private void OnButtonCreateUnits()=> CustomSceneManager.Get().LoadScene("UnitsCreator");
    private void OnButtonSetting() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Settings, (int) Menu.Main));
    private void OnButtonCredits() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Credits, (int) Menu.Main));
    private void OnButtonProfile() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Profile, (int) Menu.Main));
    private void OnButtonBarracks() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Barracks, (int) Menu.Main));
    private void OnButtonBackSettings() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main, (int) Menu.Settings));
    private void OnButtonBackCredits() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main, (int) Menu.Credits));
    private void OnButtonBackProfile() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main, (int) Menu.Profile));
    private void OnButtonBackBarracks() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main, (int) Menu.Barracks));

    private void EnableMusic()
    {
        AudioManager.Get().EnableMusic();
        btnMusicOnOff.image.sprite = AudioManager.Get().isMusicOn ? spriteAudioOn : spriteAudioOff;
    }

    private void EnableEffect()
    {
        AudioManager.Get().EnableEffect();
        btnEffectOnOff.image.sprite = AudioManager.Get().isEffectOn? spriteAudioOn : spriteAudioOff;
    }

    private IEnumerator SwitchPanel(float maxTime, int onMenu, int offMenu)
    {
        float onTime = 0;
        CanvasGroup on = menues[onMenu];
        on.gameObject.SetActive(true);
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

        off.gameObject.SetActive(false);
        currentMenu = (Menu)onMenu;
    }
}