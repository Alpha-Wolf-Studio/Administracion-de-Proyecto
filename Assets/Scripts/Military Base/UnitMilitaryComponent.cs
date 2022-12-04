using UnityEngine;
using UnityEngine.UI;

public class UnitMilitaryComponent : MonoBehaviour
{
    public Image imageUnit;
    public Image imageFillLife;
    public GameObject imageContentLife;

    private float startFillAmount;
    private float maxTimeAnimationHeal = 0.5f;
    float healingTime = 10;

    private void OnEnable ()
    {
        imageContentLife.SetActive(false);
    }

    private void Start ()
    {
        GameManager.Get().OnHealtAllUnits += SetFullLife;
    }
    
    private void Update ()
    {
        if (healingTime < maxTimeAnimationHeal)
        {
            healingTime += Time.deltaTime;
            float lerp = healingTime / maxTimeAnimationHeal;

            if (lerp > startFillAmount)
                imageFillLife.fillAmount = lerp;
        }
    }

    private void SetFullLife ()
    {
        healingTime = 0;
        startFillAmount = imageFillLife.fillAmount;
    }

    public void ShowLifeBar (bool isEnable)
    {
        imageContentLife.SetActive(isEnable);
    }

    public void UpdateFillLife (int maxLife, float currentLife)
    {
        if (healingTime < maxTimeAnimationHeal)
            return;
        imageContentLife.SetActive(true);
        imageFillLife.fillAmount = currentLife / maxLife;
    }
}