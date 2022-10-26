using UnityEngine;
using UnityEngine.UI;

public class UiManagerCampaign : MonoBehaviour
{
    [SerializeField] private Button btnGoToGameplay;
    [SerializeField] private Button btnGoToMilitaryBase;

    private void Start ()
    {
        btnGoToGameplay.onClick.AddListener(GoToMenuGameplay);
        btnGoToMilitaryBase.onClick.AddListener(GoToMenuMilitaryBase);
    }

    private void GoToMenuGameplay () => CustomSceneManager.Get().LoadScene("Gameplay");
    private void GoToMenuMilitaryBase () => CustomSceneManager.Get().LoadScene("MilitaryBase");
}