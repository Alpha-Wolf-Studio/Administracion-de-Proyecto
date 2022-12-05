using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerCampaign : MonoBehaviour
{
    [SerializeField] private Button btnGoToGameplay;
    [SerializeField] private Button btnGoToMilitaryBase;
    [SerializeField] private AudioClip audioMilitaryBase;

    [Header("Campaign Complete")] 
    [SerializeField] private GameObject campaignCompletePanel;
    [SerializeField] private Button btnCloseCampaignCompletePanel;
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start ()
    {
        btnGoToGameplay.onClick.AddListener(GoToMenuGameplay);
        btnGoToMilitaryBase.onClick.AddListener(GoToMenuMilitaryBase);
        btnCloseCampaignCompletePanel.onClick.AddListener(CampaignCompletePanelCloseButtonPressed);
        AudioManager.Get().PlayMusic();
        
        bool isLastLevelCompleteLastLevelCampaign =  GameManager.Get().IsLastLevelCampaignEnd();
        
        if (isLastLevelCompleteLastLevelCampaign && !GameManager.Get().GetPlayerGameCompleteState())
        {
            campaignCompletePanel.SetActive(true);
            GameManager.Get().SetPlayerGameCompleteState(true);
        }
    }

    private void CampaignCompletePanelCloseButtonPressed()
    {
        campaignCompletePanel.SetActive(false);
    }
    
    public void GoToMenuGameplay ()
    {
        AudioManager.Get().PlayAudioAttack();
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