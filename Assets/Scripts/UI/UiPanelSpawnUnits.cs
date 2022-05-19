using System;
using System.Collections.Generic;
using UnityEngine;

public class UiPanelSpawnUnits : MonoBehaviour
{
    [SerializeField] private GameObject pfButton = default;
    [SerializeField] private float spawnCooldown = 5f; //TODO llevar esto a las unidades y guardarlo como un valor de cada unidad
    private TroopManager troopManagerPlayer = default;
    private List<GameObject> buttonsLoaded = new List<GameObject>();

    private void Start()
    {
        troopManagerPlayer = GamePlayManager.Get().GetPlayerTroopManager();
        LoadButtons();
    }

    private void LoadButtons()
    {
        buttonsLoaded.Clear();
        for (int i = 0; i < GameManager.Get().unitsStatsLoaded.Count; i++)
        {
            GameObject go = Instantiate(pfButton, Vector3.zero, Quaternion.identity, transform);
            buttonsLoaded.Add(go);
            go.GetComponentInChildren<UiButtonSpawnUnit>().UpdateValues(i, spawnCooldown, troopManagerPlayer);
        }
    }
}