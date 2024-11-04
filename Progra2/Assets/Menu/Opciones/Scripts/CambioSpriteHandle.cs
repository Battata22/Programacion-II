using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CambioSpriteHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider slider;
    public Sprite defaultSprite;
    public Sprite pressedSprite;
    private Image handleImage;

    void Start()
    {
        slider = GetComponent<Slider>();
        handleImage = slider.handleRect.GetComponent<Image>();
        handleImage.sprite = defaultSprite;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        handleImage.sprite = pressedSprite;  
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handleImage.sprite = defaultSprite;
    }
}