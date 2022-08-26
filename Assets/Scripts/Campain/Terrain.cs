using UnityEngine;

public class Terrain : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init ()
    {

    }
}