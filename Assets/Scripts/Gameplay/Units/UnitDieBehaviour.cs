using System.Collections.Generic;
using UnityEngine;

public class UnitDieBehaviour : UnitBehaviour
{

    [Header("Fall into Death")] 
    [SerializeField] private float timeBeforeFalling = 1f;
    [SerializeField] private float timeFalling = 2f;
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float fallAcceleration = 0f;

    private Unit unit = default;
    private bool isUnitDead = false;
    private float currentTime = 0;

    [SerializeField] private List<AudioClip> audiosDie = new List<AudioClip>();
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.OnDie += EjecuteAudioDie;
        unit.OnDie += delegate
        {
            isUnitDead = true;
        };
    }

    private void EjecuteAudioDie ()
    {
        if (!audioSource)
            return;
        AudioClip clip = audiosDie[Random.Range(0, audiosDie.Count)];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public override bool IsBehaviourExecutable() => isUnitDead;

    public override void Execute()
    {
        currentTime += Time.deltaTime;
        if (timeBeforeFalling < currentTime)
        {
            if (timeFalling > currentTime)
            {
                var ownTransform = transform;
                Vector3 ownPosition = ownTransform.position;
                fallSpeed += fallAcceleration;
                ownPosition.y -= Time.deltaTime * fallSpeed;
                ownTransform.position = ownPosition;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
    
}
