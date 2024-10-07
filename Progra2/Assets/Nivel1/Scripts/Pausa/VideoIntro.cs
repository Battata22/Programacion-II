using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] Image marco1, marco2, marco3, vida, negro;
    [SerializeField] GameObject gato, abuela, perro, player, entrada, barra, crosshair, skipBoton;
    public VideoPlayer videoPlayer;
    [SerializeField] MeshRenderer gus, hand;

    [SerializeField] TutorialManager tutorialManager;
    float waitVideo;
    bool tepeado = false;
    //[SerializeField] tiempo

    void Start()
    {
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
            GameManager.Instance.Player.UpdateTerrorFrame();
        }

        waitVideo += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.F) && tepeado == false)
        {
            SkipButton();
        }
    }

    void apagado()
    {
        gus.enabled = false;
        hand.enabled = false;
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
        skipBoton.SetActive(true);
    }

    void prendido()
    {
        gus.enabled = true;
        hand.enabled = true;
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
        skipBoton.SetActive(false);
        GameManager.Instance.Tutorial.StartPickUp();
    }

    void TPGus()
        {
        player.transform.position = entrada.transform.position;
        }

    public void SkipButton()
    {
        waitVideo = 1000;
    }
}
