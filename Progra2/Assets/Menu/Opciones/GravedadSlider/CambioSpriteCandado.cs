using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class CambioSpriteCandado : MonoBehaviour
{
    Toggle Toggle;
    [SerializeField] Sprite abierto, cerrado;
    [SerializeField] Image _bk;
    void Start()
    {
        Toggle = GetComponent<Toggle>();
    }


    void Update()
    {
        if(Toggle.isOn)
        {
            _bk.sprite = abierto;
        }
        else
        {
            _bk.sprite = cerrado;
        }
    }
}
