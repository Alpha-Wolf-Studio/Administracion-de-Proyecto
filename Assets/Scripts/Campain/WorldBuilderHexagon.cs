using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class WorldBuilderHexagon : MonoBehaviour
{

    [SerializeField] private WorldBuilderData worldData = default;
    [SerializeField] private List<HexagonTerrain> hexagons = new List<HexagonTerrain>();

    public List<HexagonTerrain> GetAllHexagons() => hexagons;

    public void CreateHexagons () 
    {
        ResetHexagons();

        Vector3 pos = Vector3.zero;
        for (int i = 0; i < worldData.Columns; i++)
        {
            bool isPar = i % 2 != 0;
            if (!isPar)
                pos.z = 0;
            else
                pos.z = -worldData.ZDistance;
            pos.x += worldData.XDistance;

            for (int j = 0; j < worldData.Rows; j++)
            {
                HexagonTerrain hexagon = Instantiate(worldData.PfHexagon);
                hexagon.transform.parent = transform;
                hexagon.transform.position = pos;
                hexagon.transform.rotation = Quaternion.identity;
                pos.z += worldData.ZDistance * 2;
                hexagons.Add(hexagon);
            }
        }
    }

    public void ResetHexagons ()
    {
        foreach (HexagonTerrain hexagon in hexagons)
        {
            DestroyImmediate(hexagon.gameObject);
        }

        hexagons.Clear();
    }

}