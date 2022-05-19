using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITeamArrow : MonoBehaviour
{
    [SerializeField] private float maxLengthMovement = 1f;
    
    private SpriteRenderer renderer = default;
    private Vector3 startPosition = Vector3.zero;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.forward = Vector3.forward;
        Vector3 newPosition = startPosition;
        newPosition.x = transform.position.x;
        newPosition.y += Mathf.PingPong(Time.time, maxLengthMovement);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

    public void SetColor(Color color) 
    {
        renderer.color = color;
    }

}
