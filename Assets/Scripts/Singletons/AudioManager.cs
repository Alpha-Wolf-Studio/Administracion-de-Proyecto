using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip audioMusic;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSfx;

    public bool isMusicOn = true;
    public bool isEffectOn = true;

    private float minVolume = -80;
    private float maxVolumeMusic = -10;
    private float maxVolumeSfx = 0;
    private float maxVolumeAnbient = -6;

    [SerializeField] private List<AudioClip> audioWin = new List<AudioClip>();
    [SerializeField] private List<AudioClip> audioLose = new List<AudioClip>();
    [SerializeField] private List<AudioClip> audioRetire = new List<AudioClip>();
    [SerializeField] private List<AudioClip> audioAttack = new List<AudioClip>();

    private void Start ()
    {
        PlayMusic();
    }

    public void PlayMusic ()
    {
        audioSourceMusic.clip = audioMusic;
        audioSourceMusic.Play();
    }

    public void EnableMusic ()
    {
        isMusicOn = !isMusicOn;
        audioMixer.SetFloat("VolMusic", isMusicOn ? maxVolumeMusic : minVolume);
    }

    public void EnableEffect ()
    {
        isEffectOn = !isEffectOn;
        audioMixer.SetFloat("VolEffect", isEffectOn ? maxVolumeSfx : minVolume);
        audioMixer.SetFloat("VolAmbient", isEffectOn ? maxVolumeAnbient : minVolume);
    }

    public void PlayAudioWin () => PlayAudio(audioSourceSfx, audioWin);
    public void PlayAudioLose () => PlayAudio(audioSourceSfx, audioLose);
    public void PlayAudioRetire () => PlayAudio(audioSourceSfx, audioRetire);
    public void PlayAudioAttack () => PlayAudio(audioSourceSfx, audioAttack);


    void PlayAudio (AudioSource audioSource, List<AudioClip> clips)
    {
        if (!audioSource)
        {
            Debug.Log("No hay AudioSource");
            return;
        }

        AudioClip clip = audioWin[Random.Range(0, audioWin.Count)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}