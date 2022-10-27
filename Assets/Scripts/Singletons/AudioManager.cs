using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip clipMainMenu;
    [SerializeField] private AudioClip clipGamePlay;
    private AudioSource audioSource;
    public bool isMusicOn = true;
    public bool isEffectOn = true;

    private AudioSource sourceMusic;
    private List<AudioSource> sourceSfx = new List<AudioSource>();

    private float minVolume = -80;
    private float maxVolume = 0;

    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusicMenu()
    {
        audioSource.clip = clipMainMenu;
        audioSource.Play();
    }

    public void PlayMusicGamePlay()
    {
        audioSource.clip = clipGamePlay;
        audioSource.Play();
    }

    public void EnableMusic()
    {
        isMusicOn = !isMusicOn;
        audioMixer.SetFloat("VolMusic", isMusicOn ? maxVolume : minVolume);
    }

    public void EnableEffect()
    {
        isEffectOn = !isEffectOn;
        audioMixer.SetFloat("VolEffect", isEffectOn ? maxVolume : minVolume);
    }
}