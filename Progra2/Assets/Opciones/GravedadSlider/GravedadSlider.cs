using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravedadSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Toggle checkBox;
    [SerializeField] float vel, ace;
    void Start()
    {
        slider = GetComponent<Slider>();
    }


    void Update()
    {
        if (checkBox.isOn == true)
        {
            if (ace <= 20)
            {
                ace += Time.deltaTime;
            }

            if (slider.value > slider.minValue)
            {
                slider.value -= (vel * Time.deltaTime * ace);
            }
        }

    }
}
