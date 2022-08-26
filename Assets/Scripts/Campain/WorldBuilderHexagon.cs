using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class WorldBuilderHexagon : MonoBehaviour
{
    private float xDistance = 2.09f;
    private float zDistance = -4.8f;
    [SerializeField] private GameObject pfHexagon;
    [SerializeField] private int spawnColumns = 0;
    [SerializeField] private int spawnRows = 0;

    [SerializeField] private bool spawnButton;

    [SerializeField] private List<GameObject> hexagons = new List<GameObject>();

    void Update ()
    {
        if (spawnButton)
        {
            spawnButton = false;
            ResetHexagons();

            Vector3 pos = Vector3.zero;
            for (int i = 0; i < spawnColumns; i++)
            {
                bool isPar = i % 2 != 0;
                if (!isPar)
                    pos.z = 0;
                else
                    pos.z = -zDistance;
                pos.x += xDistance;

                for (int j = 0; j < spawnRows; j++)
                {
                    GameObject hexagon = PrefabUtility.InstantiatePrefab(pfHexagon) as GameObject;
                    hexagon.transform.parent = transform;
                    hexagon.transform.position = pos;
                    hexagon.transform.rotation = Quaternion.identity;
                    pos.z += zDistance * 2;
                    hexagons.Add(hexagon);
                }
            }
        }
    }

    void ResetHexagons ()
    {
        foreach (GameObject hexagon in hexagons)
        {
            DestroyImmediate(hexagon.gameObject);
        }

        hexagons.Clear();
    }
}