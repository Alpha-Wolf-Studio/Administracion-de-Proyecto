using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UiImageMask : Image
{
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

    public override Material materialForRendering
    {
        get
        {
            Material mat = new Material((base.materialForRendering));
            mat.SetInt(StencilComp, (int) CompareFunction.NotEqual);
            return mat;
        }
    }
}