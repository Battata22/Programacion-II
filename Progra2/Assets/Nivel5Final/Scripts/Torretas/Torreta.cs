using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public bool activado = false, idleT = false;
    [SerializeField] GameObject cuerpolaser, laserLinea;
    [SerializeField] Animator anim;
    [SerializeField] int act = 0;
    [SerializeField] Material prendida;
    [SerializeField] Renderer[] renderers;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip activadoSFX, idle, disparo;

    #region Comment
    //[SerializeField] float alturaLaser;
    //[SerializeField] LineRenderer linea;
    //[SerializeField] GameObject puntaLaser, cabezaPointer;

    //private void Awake()
    //{
    //    linea = Instantiate(linea, new Vector3(), Quaternion.identity);
    //    GameManager.Instance.firstVisualLinea = null;

    //} 
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (activado == true)
        {
            IronMan(act);
            //LineaActivar();
            if (noBucle == false)
            {
                ActivarLaser();
            }
            CambioDePantalones();

        }

        if (idleT == true && audioSource.isPlaying == false)
        {
            audioSource.loop = true;
            audioSource.clip = idle;
            audioSource.Play();
        }

        if (GameManager.Instance.GB_BossScript.paraAudio == true && noBucle == false)
        {
            AudioShoot();
            noBucle = true;
            Invoke("DesactivarLaser", 0.5f);
        }
    }

    bool noBucle = false;
    public void AudioShoot()
    {
        idleT = false;
        audioSource.loop = false;
        audioSource.clip = disparo;
        audioSource.Play();
    }

    void IronMan(int rep)
    {
        if (rep == 0)
        {
            //ACTIVAR SONIDO
            print("antes del clip");
            audioSource.clip = activadoSFX;
            print("despues del clip");
            audioSource.Play();
            print("despues del play");

            anim.SetBool("ActivadoAnim", true);
            TorretaPadre.activadas++;

            act++;
            idleT = true;
        }

    }

    #region Comment
    //public void LineaActivar()
    //{
    //    linea.enabled = true;
    //    linea.SetPosition(0, cabezaPointer.transform.position);
    //    //linea.SetPosition(0, puntaLaser.transform.forward);
    //    linea.SetPosition(1, puntaLaser.transform.position);
    //} 
    #endregion

    public void ActivarLaser()
    {
        if (laserLinea.GetComponent<MeshRenderer>().enabled == false)
        {
            laserLinea.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void DesactivarLaser()
    {
        print("metodo");
        if (laserLinea.GetComponent<MeshRenderer>().enabled == true)
        {
            laserLinea.GetComponent<MeshRenderer>().enabled = false;
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
