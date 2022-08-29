using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexagonTerrain))]
public class TerrainAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshRenderer baseMesh = default;
    [SerializeField] private MeshRenderer stateMesh = default;
    [SerializeField] private MeshRenderer provinceMesh = default;
    [Header("Configurations")]
    [SerializeField] private float maxYOffset = 1;
    [SerializeField] private float speed = 1;

    private HexagonTerrain terrain = default;

    Vector3 baseMeshStartPosition = Vector3.zero;
    Vector3 stateMeshStartPosition = Vector3.zero;
    Vector3 provinceMeshStartPosition = Vector3.zero;

    private IEnumerator animationIEnumerator = default;

    private void Awake ()
    {
        terrain = GetComponent<HexagonTerrain>();

        baseMeshStartPosition = baseMesh.transform.position;
        stateMeshStartPosition = stateMesh.transform.position;
        provinceMeshStartPosition = provinceMesh.transform.position;

        terrain.OnSelect += delegate
        {
            if (animationIEnumerator != null) StopCoroutine(animationIEnumerator);
            animationIEnumerator = SelectAnimationCoroutine();
            StartCoroutine(animationIEnumerator);
        };

        terrain.OnDeSelect += delegate
        {
            if (animationIEnumerator != null) StopCoroutine(animationIEnumerator);
            animationIEnumerator = DeSelectAnimationCoroutine();
            StartCoroutine(animationIEnumerator);
        };
    }

    private IEnumerator SelectAnimationCoroutine() 
    {
        float t = 0;
        Vector3 currentBaseMeshStartPosition = baseMesh.transform.position;
        Vector3 currentStateMeshStartPosition = stateMesh.transform.position;
        Vector3 currentProvinceMeshStartPosition = provinceMesh.transform.position;

        Vector3 targetBaseMeshStartPosition = baseMeshStartPosition;
        targetBaseMeshStartPosition.y += maxYOffset;

        Vector3 targetStateMeshStartPosition = stateMeshStartPosition;
        targetStateMeshStartPosition.y += maxYOffset * 2 / 3;

        Vector3 targetProvinceMeshStartPosition = provinceMeshStartPosition;
        targetProvinceMeshStartPosition.y += maxYOffset / 3;

        while (t < 1) 
        {

            baseMesh.transform.position = Vector3.Lerp(currentBaseMeshStartPosition, targetBaseMeshStartPosition, t);
            stateMesh.transform.position = Vector3.Lerp(currentStateMeshStartPosition, targetStateMeshStartPosition, t);
            provinceMesh.transform.position = Vector3.Lerp(currentProvinceMeshStartPosition, targetProvinceMeshStartPosition, t);

            t += Time.deltaTime * speed;
            yield return null;
        }
    }

    private IEnumerator DeSelectAnimationCoroutine() 
    {
        float t = 0;
        Vector3 currentBaseMeshStartPosition = baseMesh.transform.position;
        Vector3 currentStateMeshStartPosition = stateMesh.transform.position;
        Vector3 currentProvinceMeshStartPosition = provinceMesh.transform.position;

        Vector3 targetBaseMeshStartPosition = baseMeshStartPosition;
        Vector3 targetStateMeshStartPosition = stateMeshStartPosition;
        Vector3 targetProvinceMeshStartPosition = provinceMeshStartPosition;

        while (t < 1)
        {

            baseMesh.transform.position = Vector3.Lerp(currentBaseMeshStartPosition, targetBaseMeshStartPosition, t);
            stateMesh.transform.position = Vector3.Lerp(currentStateMeshStartPosition, targetStateMeshStartPosition, t);
            provinceMesh.transform.position = Vector3.Lerp(currentProvinceMeshStartPosition, targetProvinceMeshStartPosition, t);

            t += Time.deltaTime * speed;
            yield return null;
        }
    }

}
