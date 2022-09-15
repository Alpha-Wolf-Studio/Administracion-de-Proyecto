using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform limitLeft;
    [SerializeField] private Transform limitRight;
    [Header("Settings: ")]
    [SerializeField] private float dragSpeed = 2;
    private Vector3 dragStart;

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (Utils.IsPointerOverUIObject()) return;
        if (!Input.GetMouseButton(0)) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;
        }

        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragStart);
        Vector3 move = new Vector3(pos.x * -dragSpeed, 0, 0);

        cam.transform.Translate(move, Space.World);

        if (cam.transform.position.x < limitLeft.position.x)
            cam.transform.position = limitLeft.transform.position;
        else if (cam.transform.position.x > limitRight.position.x)
            cam.transform.position = limitRight.transform.position;
    }
}