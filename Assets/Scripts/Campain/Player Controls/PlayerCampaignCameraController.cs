using System.Collections;
using UnityEngine;

public class PlayerCampaignCameraController : MonoBehaviour
{

    [Header("Controls Settings")]
    [SerializeField] private Vector3 distanceOffset = Vector3.zero;
    [SerializeField] private float speed = 1;

    private IEnumerator changeTargetIEnumerator = default;
    private Transform currentTarget = default;

    public void SetTarget(Transform newTarget) 
    {
        if (newTarget == currentTarget) return;
        currentTarget = newTarget;

        if (changeTargetIEnumerator != null) StopCoroutine(changeTargetIEnumerator);
        changeTargetIEnumerator = ChangeTargetCoroutine();
        StartCoroutine(changeTargetIEnumerator);
    }

    private IEnumerator ChangeTargetCoroutine() 
    {
        float t = 0;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentTarget.position + distanceOffset;

        while(t < 1) 
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += Time.deltaTime * speed;
            yield return null;
        }
    }

}
