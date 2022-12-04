using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile General")]
    [SerializeField] protected int groundLayer = 9;
    private AudioSource audioSource;

    protected Unit unitShooter;
    protected LayerMask maskToDamage;
    protected float damage;
    protected float velocity = 5;
    protected List<AudioClip> audioImpact;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void SetAttributes(Unit shooter, Collider target = null);
    public abstract void StartProjectile();
    public abstract void DestroyProjectile();

    public void SetAudios (List<AudioClip> audioShoots, List<AudioClip> audioImpact)
    {
        PlayAudio(audioShoots);
        this.audioImpact = audioImpact;
    }

    protected void PlayAudio (List<AudioClip> audios)
    {
        if (!audioSource)
            return;
        audioSource.Stop();
        AudioClip clip = audios[Random.Range(0, audios.Count)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}