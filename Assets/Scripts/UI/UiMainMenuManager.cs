using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class UiMainMenuManager : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private CanvasGroup[] menues;

    [Header("References Menu Buttons: ")] 
    [SerializeField] private Button btnToMissions;
    [SerializeField] private Button btnToCreator;
    [SerializeField] private Button btnToSettings;
    [SerializeField] private Button btnToCredits;
    [SerializeField] private Button btnToProfile;
    [SerializeField] private Button btnToBarracks;

    [SerializeField] private Button[] buttonsToMenu;

    [Header("Reference Options Buttons")]
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
        AddListeners();
        textVersion.text = "Version: " + Application.version;
        btnMusicOnOff.image.sprite = AudioManager.Get().isMusicOn ? spriteAudioOn : spriteAudioOff;
        btnEffectOnOff.image.sprite = AudioManager.Get().isEffectOn ? spriteAudioOn : spriteAudioOff;
    }

    private void AddListeners()
    {
        btnToMissions.onClick.AddListener(OnButtonPlay);
        btnToCreator.onClick.AddListener(OnButtonCreateUnits);
        btnToSettings.onClick.AddListener(OnButtonSetting);
        btnToCredits.onClick.AddListener(OnButtonCredits);
        btnToProfile.onClick.AddListener(OnButtonProfile);
        btnToBarracks.onClick.AddListener(OnButtonBarracks);

        foreach (var button in buttonsToMenu)
        {
            button.onClick.AddListener(OnButtonToMainMenu);
        }

        btnMusicOnOff.onClick.AddListener(EnableMusic);
        btnEffectOnOff.onClick.AddListener(EnableEffect);
    }

    private void OnButtonPlay() => CustomSceneManager.Get().LoadScene("Campaign");
    private void OnButtonCreateUnits()=> CustomSceneManager.Get().LoadScene("UnitsCreator");
    private void OnButtonSetting() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Settings));
    private void OnButtonCredits() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Credits));
    private void OnButtonProfile() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Profile));
    private void OnButtonBarracks() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Barracks));
    private void OnButtonToMainMenu() => StartCoroutine(SwitchPanel(transitionTime, (int) Menu.Main));

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

    private IEnumerator SwitchPanel(float maxTime, int onMenu)
    {
        float onTime = 0;
        CanvasGroup on = menues[onMenu];
        on.gameObject.SetActive(true);
        CanvasGroup off = menues[(int)currentMenu];

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