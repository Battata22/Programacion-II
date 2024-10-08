using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamaraCanvas : MonoBehaviour
{
    [SerializeField] RawImage imagen;
    float wait;

    private void Start()
    {
        imagen = GetComponent<RawImage>();

        GameManager.Instance.CamGBCanvas = this;
    }
    void Update()
    {
        wait += Time.deltaTime;

        if(wait >= 3f)
        {
            ApagadoRAW();
        }
    }

    public void ApagadoRAW()
    {
        imagen.enabled = false;
    }

    public void PrendidoRAW()
    {
        wait = 0;
        imagen.enabled = true;
    }
}
