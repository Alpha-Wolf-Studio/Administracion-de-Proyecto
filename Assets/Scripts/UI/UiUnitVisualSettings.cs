using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUnitVisualSettings : MonoBehaviourSingleton<UiUnitVisualSettings>
{
    public Action OnUpdateUI;
    [SerializeField] private MeshFilter modelReference;
    [SerializeField] private List<Slider> sliderRGB = new List<Slider>();
    
    private MeshRenderer modelRend;
    private Color color = Color.black;
    private Mesh mesh;
    private CurrentShape currentShape = CurrentShape.Sphere;


    private void Start()
    {
        modelRend = modelReference.GetComponent<MeshRenderer>();
        OnButtonSetShape(0);
        SetColorRed(0);
        SetColorGreen(0);
        SetColorBlue(0);
    }
    public void SetColorRed(float value)
    {
        color.r = value;
        modelRend.material.color = color;
        UpdateUI();
    }
    public void SetColorGreen(float value)
    {
        color.g = value;
        modelRend.material.color = color;
        UpdateUI();
    }
    public void SetColorBlue(float value)
    {
        color.b = value;
        modelRend.material.color = color;
        UpdateUI();
    }
    public void OnButtonSetShape(int index)
    {
        mesh = GameManager.Get().GetCurrentMesh(index);
        if (!mesh)
        {
            Debug.LogWarning("No está seteado el botón y el index se pasa del tamaño de meshes!!!", gameObject);
            return;
        }

        currentShape = (CurrentShape) index;
        modelReference.mesh = mesh;
        UpdateUI();
    }
    public Color GetColor() => color;
    public CurrentShape GetCurrentShape() => currentShape;

    public void SetColor(Color newColor)
    {
        color = newColor;
        UpdateUI();
    }

    public void SetMesh(CurrentShape newCurrentShape)
    {
        currentShape = newCurrentShape;
        OnButtonSetShape((int) currentShape);
    }

    void UpdateUI()
    {
        sliderRGB[0].value = color.r;
        sliderRGB[1].value = color.g;
        sliderRGB[2].value = color.b;
        OnUpdateUI?.Invoke();
    }
}