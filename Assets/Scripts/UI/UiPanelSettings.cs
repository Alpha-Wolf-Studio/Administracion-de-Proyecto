using UnityEngine;
using UnityEngine.UI;

public class UiPanelSettings : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnMusicOnOff;
    [SerializeField] private Button btnEffectOnOff;
    [SerializeField] private Sprite spriteAudioOn;
    [SerializeField] private Sprite spriteAudioOff;

    private void Start()
    {
        btnBack.onClick.AddListener(OnPressBack);

        btnMusicOnOff.image.sprite = AudioManager.Get().isMusicOn ? spriteAudioOn : spriteAudioOff;
        btnEffectOnOff.image.sprite = AudioManager.Get().isEffectOn ? spriteAudioOn : spriteAudioOff;

        btnMusicOnOff.onClick.AddListener(EnableMusic);
        btnEffectOnOff.onClick.AddListener(EnableEffect);
    }

    private void OnPressBack()
    {
        gameObject.SetActive(false);
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