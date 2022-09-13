using System.Collections;
using UnityEngine;

public class PlayerCampaignCameraController : MonoBehaviour
{

    [Header("Controls Settings")]
    [SerializeField] private Vector3 distanceOffset = Vector3.zero;
    [SerializeField] private float autoMovementSpeed = 5;
    [SerializeField] private float panSpeed = 1;

    private IEnumerator changeTargetIEnumerator = default;
    private Transform currentTarget = default;
    private Camera cam = default;

    private void Awake()
    {
        cam = Camera.main;    
    }

    public void SetTarget(Transform newTarget) 
    {
        if (newTarget == currentTarget) return;
        currentTarget = newTarget;

        if (changeTargetIEnumerator != null) StopCoroutine(changeTargetIEnumerator);
        changeTargetIEnumerator = ChangeTargetCoroutine();
        StartCoroutine(changeTargetIEnumerator);
    }

    public void MoveCamera(Vector3 direction) 
    {
        if (changeTargetIEnumerator != null) StopCoroutine(changeTargetIEnumerator);
        transform.position += direction * panSpeed;
    }

    private IEnumerator ChangeTargetCoroutine() 
    {
        float t = 0;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentTarget.position + distanceOffset;

        while(t < 1) 
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += Time.deltaTime * autoMovementSpeed;
            yield return null;
        }
    }

}
