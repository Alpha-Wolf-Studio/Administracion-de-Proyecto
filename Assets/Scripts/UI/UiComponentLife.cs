using UnityEngine;
using UnityEngine.UI;

public class UiComponentLife : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image imageFill;
    private void Start()
    {
        unit.OnTakeDamage += TakeDamageUI;
    }
    void TakeDamageUI(float currentLife, float maxLife)
    {
        imageFill.fillAmount = currentLife / maxLife;
    }
}