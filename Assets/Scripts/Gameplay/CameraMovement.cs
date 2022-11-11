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

        Transform cameraTransform = cam.transform;
        cameraTransform.Translate(move, Space.World);

        Vector3 cameraPosition = cameraTransform.position;

        if (cameraPosition.x < limitLeft.position.x)
            cameraPosition.x = limitLeft.position.x;
        
        else if (cameraPosition.x > limitRight.position.x)
            cameraPosition.x = limitRight.position.x;

        cameraTransform.position = cameraPosition;
    }
}