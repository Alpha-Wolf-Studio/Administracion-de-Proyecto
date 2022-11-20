using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexagonTerrain))]
public class TerrainAnimation : MonoBehaviour
{

    [Header("Animation Configuration")]
    [SerializeField] private float animationSpeed = 2f;
    [SerializeField] private Vector3 offsetPosition = Vector3.zero;
    
    private Vector3 startPosition;
    
    private HexagonTerrain terrain = default;
    
    private void Awake ()
    {

        startPosition = transform.position;
        
        terrain = GetComponent<HexagonTerrain>();
        
        terrain.OnSelect += delegate
        {
            StopAllCoroutines();
            StartCoroutine(SelectCoroutine());
        };

        terrain.OnDeSelect += delegate
        {
            StopAllCoroutines();
            StartCoroutine(DeSelectCoroutine());
        };
    }

    private IEnumerator SelectCoroutine()
    {
        float t = 0;
        Vector3 currentPosition = transform.position;
        Vector3 endPosition = startPosition + offsetPosition;
        
        while (t < 1)
        {
            transform.position = Vector3.Lerp(currentPosition, endPosition, t);
            t += Time.deltaTime * animationSpeed;
            yield return null;
        }

        transform.position = endPosition;
    }

    private IEnumerator DeSelectCoroutine()
    {
        float t = 0;
        Vector3 currentPosition = transform.position;
        
        while (t < 1)
        {
            transform.position = Vector3.Lerp(currentPosition, startPosition, t);
            t += Time.deltaTime * animationSpeed;
            yield return null;
        }

        transform.position = startPosition;
    }

}
