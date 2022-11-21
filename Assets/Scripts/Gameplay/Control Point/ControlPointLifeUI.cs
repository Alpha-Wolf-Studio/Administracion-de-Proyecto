using UnityEngine;

public class ControlPointLifeUI : MonoBehaviour
{
    [SerializeField] private Unit controlPointUnit;
    [SerializeField] private MeshRenderer[] flagRenderers;

    private const float MinOffsetY = -.31f;
    private const float MaxOffsetY = .34f;
    
    private void Start()
    {
        controlPointUnit.OnTakeDamage += delegate(float currentLife, float maxLife)
        {
            foreach (var rend in flagRenderers)
            {
                if(!rend.gameObject.activeSelf) return;
                Vector2 offset = rend.materials[1].mainTextureOffset;
                offset.y = Mathf.Lerp (MaxOffsetY, MinOffsetY, Mathf.InverseLerp (0, maxLife, currentLife));
                rend.materials[1].mainTextureOffset = offset;
            }
        };
        
        controlPointUnit.OnDie += delegate
        {
            foreach (var rend in flagRenderers)
            {
                if(!rend.gameObject.activeSelf) return;
                Vector2 offset = rend.materials[1].mainTextureOffset;
                offset.y = MaxOffsetY;
                rend.materials[1].mainTextureOffset = offset;
            }
        }; 
    }
}
