using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Lane : MonoBehaviour
{

    public Action<int> OnLaneSelected;

    [Header("Flash Configurations")]
    [SerializeField] private float flashSpeed = 1f;
    [SerializeField] private Color selectedColor = default;
    [SerializeField] private Color deSelectedColor = default;
    [Header("Spawn Configurations")]
    [SerializeField] private Transform startTransform = default;
    [SerializeField] private LanesFlags laneFlag = default;

    public LanesFlags LaneFlags => laneFlag;
    public void SetLaneIndex(int newLaneIndex) => laneIndex = newLaneIndex;
    public Vector3 StartPosition
    {
        get => startPosition;
        set
        {
            startPosition = value;
            startTransform.position = startPosition;
        }
    }
    public bool Selected 
    {
        set 
        {
            selected = value;
            SelectLogic();
        }
    }

    private Material material;
    private bool selected = false;
    private int laneIndex = 0;
    private Vector3 startPosition = Vector3.zero;


    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = deSelectedColor;
    }

    private void Start()
    {
        StartPosition = startTransform.position;
    }


    private void SelectLogic() 
    {
        if (selected) 
        {
            material.color = selectedColor;
            StartCoroutine(LaneFlashCoroutine());
        }
        else 
        {
            material.color = deSelectedColor;
        }
    }
    
    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    

    private void OnMouseDown()
    {
        if (IsPointerOverUIObject()) return;
        OnLaneSelected?.Invoke(laneIndex);
    }

    private IEnumerator LaneFlashCoroutine() 
    {
        while (selected) 
        {   
            material.color = Color.Lerp(deSelectedColor, selectedColor, Mathf.PingPong(Time.time * flashSpeed, 1));
            yield return null;
        }
    }

}
