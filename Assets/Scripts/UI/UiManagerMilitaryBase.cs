using UnityEngine;
using UnityEngine.UI;

public class UiManagerMilitaryBase : MonoBehaviour
{
    [SerializeField] private Button btnGoToCampaing;

    void Start ()
    {
        btnGoToCampaing.onClick.AddListener(GoToMenuCampaing);
    }

    void GoToMenuCampaing ()=> CustomSceneManager.LoadScene("Campaign");
    
}