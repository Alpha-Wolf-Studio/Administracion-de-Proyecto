using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class Lane : MonoBehaviour
{

    public System.Action<int> OnLaneSelected;

    [Header("Flash Configurations")]
    [SerializeField] private float flashSpeed = 1f;
    [SerializeField] private Color selectedColor = default;
    [SerializeField] private Color deSelectedColor = default;
    [Header("Spawn Configurations")]
    [SerializeField] private Transform startTransform = default;

    public Transform StartTransform 
    {
        get 
        {
            return startTransform;
        }
        set 
        {
            startTransform = value;
        }
    }


    private Material material;
    private bool selected = false;
    private int laneIndex = 0;

    public void SetLaneIndex(int laneIndex) => this.laneIndex = laneIndex;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = deSelectedColor;
    }

    public bool Selected 
    {
        get 
        {
            return selected;
        }
        set 
        {
            selected = value;
            SelectLogic();
        }
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

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
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
