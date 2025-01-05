using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVida : MonoBehaviour
{
    [SerializeField] GB_Boss bossScript;
    [SerializeField] Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = bossScript.maxHp;
    }


    void Update()
    {
        slider.value = bossScript.actualHp;
    }
}
