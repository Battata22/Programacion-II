using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonLaser : MonoBehaviour
{
    [SerializeField] Material verde, rojo;
    [SerializeField] Renderer renderer_;

    void Start()
    {
        renderer_ = GetComponent<Renderer>();
        renderer_.material = rojo;
    }


    void Update()
    {
       if (TorretaPadre.activadas >= 3)
       {
            renderer_.material = verde;
       } 
    }

    public void PressBoton()
    {
        if(TorretaPadre.activadas == 3)
        {
            print("you win bitch");
            GameManager.Instance.GB_BossScript.Kill();
        }
        else if (TorretaPadre.activadas < 3)
        {
            print("botun aputrado pero no estan todos activados");
        }
        else
        {
            print("que carajo hiciste hermano");
            //Physics.gravity = new Vector3(0, -9.8f, 0);
        }

    }

}
