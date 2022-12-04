using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerCampaign : MonoBehaviour
{
    [SerializeField] private Button btnGoToGameplay;
    [SerializeField] private Button btnGoToMilitaryBase;
    [SerializeField] private AudioClip audioMilitaryBase;
    [SerializeField] private List<AudioClip> audioAttack=new List<AudioClip>();
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start ()
    {
        btnGoToGameplay.onClick.AddListener(GoToMenuGameplay);
        btnGoToMilitaryBase.onClick.AddListener(GoToMenuMilitaryBase);
        AudioManager.Get().PlayMusicMenu();
    }

    public void GoToMenuGameplay ()
    {
        audioSource.clip = audioAttack[Random.Range(0, audioAttack.Count)];
        audioSource.Play();
        CustomSceneManager.Get().LoadScene("Gameplay");
    }

    public void GoToMenuMilitaryBase ()
    {
        if (!audioSource)
            return;
        audioSource.clip = audioMilitaryBase;
        audioSource.Play();
        CustomSceneManager.Get().LoadScene("MilitaryBase");
    }
}