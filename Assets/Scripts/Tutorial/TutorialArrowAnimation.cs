using UnityEngine;

public class TutorialArrowAnimation : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 initAnchoredPosition;
    [SerializeField] private Vector2 endAnchoredPosition;
    [SerializeField] private float maxTimeAnimation = 1.0f;
    [SerializeField] private AnimationCurve animationCurve;
    private float onTime;

    private void Awake ()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        initAnchoredPosition = rectTransform.anchoredPosition;
    }

    void Update ()
    {
        onTime += Time.deltaTime;
        if (onTime > maxTimeAnimation)
            onTime -= maxTimeAnimation;

        float lerp = animationCurve.Evaluate(onTime / maxTimeAnimation);
        rectTransform.anchoredPosition = Vector2.Lerp(initAnchoredPosition, endAnchoredPosition, lerp);
    }
}