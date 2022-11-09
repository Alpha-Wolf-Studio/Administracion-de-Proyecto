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
        AudioManager.Get().PlayMusicMenu();
    }

    public void GoToMenuGameplay () => CustomSceneManager.Get().LoadScene("Gameplay");
    public void GoToMenuMilitaryBase () => CustomSceneManager.Get().LoadScene("MilitaryBase");
}