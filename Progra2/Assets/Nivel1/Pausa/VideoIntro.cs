using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] Image marco1, marco2, marco3, vida, negro;
    [SerializeField] GameObject gato, abuela, perro, player, entrada, barra, crosshair;
    [SerializeField] VideoPlayer videoPlayer;
    float waitVideo;
    bool tepeado = false;
    //[SerializeField] tiempo

    void Start()
    {
        //desactivar todo lo que se tenga que desactivar antes de que empiece el nivel 1 como el perro, gato, abuela, GB, tiempo, bebes, etc, UI.
        //mostrar el video
        //cuando el video termine activar las cosas
        apagado();
        waitVideo = 0;
        videoPlayer.Play();
        tepeado = false;


    }


    void Update()
    {
        if (waitVideo >= videoPlayer.length && tepeado == false)
        {
            prendido();
            TPGus();
            tepeado = true;
            GameManager.Instance.Player.Marco1();
        }
        waitVideo += Time.deltaTime;
    }

    void apagado()
    {
        marco1.enabled = false;
        marco2.enabled = false;
        marco3.enabled = false;
        vida.enabled = false;
        crosshair.SetActive(false);
        barra.SetActive(false);
        gato.SetActive(false);
        perro.SetActive(false);
        abuela.SetActive(false);
        negro.enabled = false;
    }

    void prendido()
    {
        marco1.enabled = true;
        marco2.enabled = true;
        marco3.enabled = true;
        vida.enabled = true;
        crosshair.SetActive(true);
        barra.SetActive(true);
        gato.SetActive(true);
        perro.SetActive(true);
        abuela.SetActive(true);
        videoPlayer.enabled = false;
    }

    void TPGus()
        {
        player.transform.position = entrada.transform.position;
        }
}
