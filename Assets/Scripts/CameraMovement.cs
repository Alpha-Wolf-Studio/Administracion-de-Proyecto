using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Camera cam;
    private Vector3 initialPos;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        initialPos = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, 0, 0);

        transform.Translate(move, Space.World);
    }
}