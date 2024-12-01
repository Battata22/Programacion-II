using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SistemaDeNextItem : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    public bool isNext = false;
    MeshRenderer meshR;
    public bool nivel1 = false, nivel2 = false;
    [SerializeField] bool candado = false, canilla1 = false, heladera = false, final1 = false;
    [SerializeField] bool canilla = false, ducha = false, cajas = false, final = false;

    private void Start()
    {
        meshR = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        if (nivel1)
        {
            if (GameManager.Instance.pasoActual == 1 && canilla)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 2 && ducha)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 3 && cajas)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 4 && final)
            {
                Anim();
            }
            else
            {
                meshR.enabled = false;
            }
        }
        else if (nivel2)
        {
            if (GameManager.Instance.pasoActual == 0 && candado)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 1 && canilla1)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 2 && heladera)
            {
                Anim();
            }
            else if (GameManager.Instance.pasoActual == 3 && final1)
            {
                Anim();
            }
            else
            {
                meshR.enabled = false;
            }
        }

    }

    void Anim()
    {
        meshR.enabled = true;
        transform.eulerAngles += new Vector3(0, 0, rotSpeed) * Time.fixedDeltaTime;
    }
}
