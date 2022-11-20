using System;
using UnityEngine;

public class TerrainGraphicBase : MonoBehaviour
{
    
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetBaseMaterialColor(Color color)
    {
        rend.material.color = color;
    }
    
}
