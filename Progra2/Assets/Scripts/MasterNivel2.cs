using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterNivel2 : MonoBehaviour
{
    public GameObject gb, exo;
    void Start()
    {
        GameManager.Instance._master2 = this;
    }

    public void ActivarGB()
    {
        Invoke("Exo", 1.46f);
        GameManager.Instance.AnimPuerta.SetTrigger("GB_Arrives");
        GameManager.Instance.CamGBCanvas.PrendidoRAW();
    }

    void GB()
    {
        gb.SetActive(true);
    }

    void Exo()
    {
        exo.SetActive(true);
    }
}
