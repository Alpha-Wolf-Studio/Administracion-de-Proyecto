using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    [SerializeField] private List<Terrain> terrains = new List<Terrain>();

    void Start()
    {
        foreach (Terrain terrain in terrains)
        {
            terrain.Init();
        }
    }
}