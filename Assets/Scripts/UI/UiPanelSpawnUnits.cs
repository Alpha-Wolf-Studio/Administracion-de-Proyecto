using System.Collections.Generic;
using UnityEngine;

public class UiPanelSpawnUnits : MonoBehaviour
{
    [SerializeField] private GameObject pfButton;
    [SerializeField] private TroopManager troopManagerPlayer;
    private List<GameObject> buttonsLoaded = new List<GameObject>();

    void Start()
    {
        GameManager.Get().OnLoadedStats += LoadButtons;
    }
    void LoadButtons()
    {
        buttonsLoaded.Clear();
        for (int i = 0; i < GameManager.Get().unitsStatsLoaded.Count; i++)
        {
            GameObject go = Instantiate(pfButton, Vector3.zero, Quaternion.identity, transform);
            buttonsLoaded.Add(go);
            go.GetComponent<UiButtonSpawnUnit>().UpdateValues(i, troopManagerPlayer);
        }
    }
}