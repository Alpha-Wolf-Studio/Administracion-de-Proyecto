using UnityEngine;
using UnityEngine.UI;

public class UiPanelSettings : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnCreditsBack;
    [SerializeField] private Button btnMusicOnOff;
    [SerializeField] private Button btnEffectOnOff;

    [Header("Panels")] 
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Sprite References")]
    [SerializeField] private Sprite spriteAudioOn;
    [SerializeField] private Sprite spriteAudioOff;

    private void Start()
    {
        btnBack.onClick.AddListener(OnPressBack);
        btnCredits.onClick.AddListener(OnPressCredits);
        btnCreditsBack.onClick.AddListener(OnPressCreditsBack);

        btnMusicOnOff.image.sprite = AudioManager.Get().isMusicOn ? spriteAudioOn : spriteAudioOff;
        btnEffectOnOff.image.sprite = AudioManager.Get().isEffectOn ? spriteAudioOn : spriteAudioOff;

        btnMusicOnOff.onClick.AddListener(EnableMusic);
        btnEffectOnOff.onClick.AddListener(EnableEffect);
    }

    private void OnPressBack()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnPressCredits()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void OnPressCreditsBack()
    {
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    
    private void EnableMusic()
    {
        AudioManager.Get().EnableMusic();
        btnMusicOnOff.image.sprite = AudioManager.Get().isMusicOn ? spriteAudioOn : spriteAudioOff;
    }

    private void EnableEffect()
    {
        AudioManager.Get().EnableEffect();
        btnEffectOnOff.image.sprite = AudioManager.Get().isEffectOn ? spriteAudioOn : spriteAudioOff;
    }
}