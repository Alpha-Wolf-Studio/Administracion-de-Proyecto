using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateUnit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float speed = 2;
    private bool isRotating;

    private void Update ()
    {
        if (isRotating)
            RotatePivot();
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        isRotating = true;
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        isRotating = false;
        //ResetRotation();
    }

    void RotatePivot ()
    {
        pivot.transform.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);
    }

    void ResetRotation ()
    {
        Vector3 angle = new Vector3(0, 90, 0);
        pivot.transform.rotation = Quaternion.Euler(angle);
    }
}