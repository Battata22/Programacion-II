using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagenRojaFadeOut : MonoBehaviour
{
    Color colorBase;
    private void Awake()
    {
        GameManager.Instance.imagenRojo = gameObject;
        gameObject.SetActive(false);

        Image im = GetComponent<Image>();
        colorBase = im.color;
    }
    void Update()
    {
        Image im = GetComponent<Image>();

        im.color -= new Color(0, 0, 0, 1.5f) * Time.deltaTime;
        if (im.color.a <= 0)
        {
            im.color = colorBase;
            gameObject.SetActive(false);
        }
    }
}
