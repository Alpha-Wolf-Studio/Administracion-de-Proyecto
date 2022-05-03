using UnityEngine;

public class UITrenchReleaseButton : MonoBehaviour
{
    public System.Action OnButtonPressed;

    private void OnMouseDown() => OnButtonPressed?.Invoke();

}
