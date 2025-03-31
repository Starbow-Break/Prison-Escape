using UnityEngine;
using UnityEngine.UI;

public class AimUI : MonoBehaviour
{
    [SerializeField, Min(1)] float unfocusSizeMultiplier = 2.0f;
    [SerializeField, Min(1)] float focusSizeMultiplier = 4.0f;
    [SerializeField] private Sprite focusableSprite;
    [SerializeField] private Sprite unfocusableSprite;

    RectTransform aimRect;
    Image aimImage;
    Vector2 aimOriginSize;

    public void Awake()
    {
        aimRect = GetComponent<RectTransform>();
        aimImage = GetComponent<Image>();
        aimOriginSize = aimRect.sizeDelta;
    }

    public void SetAimPoint(bool detect, bool focusableDetect)
    {
        if (detect)
        {
            if (focusableDetect)
            {
                aimRect.sizeDelta = aimOriginSize * focusSizeMultiplier;
                aimImage.sprite = focusableSprite;
            }
            else
            {
                aimRect.sizeDelta = aimOriginSize * unfocusSizeMultiplier;
                aimImage.sprite = unfocusableSprite;
            }
        }
        else
        {
            aimRect.sizeDelta = aimOriginSize;
            aimImage.sprite = unfocusableSprite;
        }
    }
}
