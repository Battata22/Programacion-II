using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public bool activado = false;
    [SerializeField] GameObject cuerpolaser, laserLinea;
    [SerializeField] Animator anim;
    [SerializeField] int act = 0;
    [SerializeField] Material prendida;
    [SerializeField] Renderer[] renderers;

    //[SerializeField] float alturaLaser;
    //[SerializeField] LineRenderer linea;
    //[SerializeField] GameObject puntaLaser, cabezaPointer;

    //private void Awake()
    //{
    //    linea = Instantiate(linea, new Vector3(), Quaternion.identity);
    //    GameManager.Instance.firstVisualLinea = null;

    //}

    void Start()
    {
        anim = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
    }


    void Update()
    {
        if (activado == true)
        {
            IronMan(act);
            //LineaActivar();
            ActivarLaser();
            CambioDePantalones();

        }
    }

    void IronMan(int rep)
    {
        if (rep == 0)
        {
            anim.SetBool("ActivadoAnim", true);
            TorretaPadre.activadas++;

            act++;
        }

    }

    //public void LineaActivar()
    //{
    //    linea.enabled = true;
    //    linea.SetPosition(0, cabezaPointer.transform.position);
    //    //linea.SetPosition(0, puntaLaser.transform.forward);
    //    linea.SetPosition(1, puntaLaser.transform.position);
    //}

    public void ActivarLaser()
    {
        if (laserLinea.GetComponent<MeshRenderer>().enabled == false)
        {
            laserLinea.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void CambioDePantalones()
    {
        for (int i = 0; i < 2; i++) 
        {
            renderers[i].material = prendida;
        }
    }
}
