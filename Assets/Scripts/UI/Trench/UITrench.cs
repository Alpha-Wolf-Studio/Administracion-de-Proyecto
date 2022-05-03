using UnityEngine;

public class UITrench : MonoBehaviour
{
    [SerializeField] private UITrenchReleaseButton releaseButton = default;
    [SerializeField] private int trenchLayerToShow = 0;
    private Trench trench;

    private void Awake()
    {
        trench = GetComponent<Trench>();
        trench.OnCurrentTroopsLayerChanged += TrenchLayerChange;
        releaseButton.OnButtonPressed += ReleaseTrench;
    }

    private void TrenchLayerChange(int layer) 
    {
        if (layer == trenchLayerToShow) releaseButton.gameObject.SetActive(true);
    }

    private void ReleaseTrench() 
    {
        trench.ReleaseUnitsFromTrench();
        releaseButton.gameObject.SetActive(false);
    }

}
