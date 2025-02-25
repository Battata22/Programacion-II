using CasaFiesta;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] bool tutorial = false, boss = false;
    [SerializeField] Image marco1, marco2, marco3, vida, negro;
    [SerializeField] GameObject player, entrada, barra, crosshair, skipBoton, postit /* gato, abuela, perro */, textoTraps, abuelaDormida, sliderBoss;
    public VideoPlayer videoPlayer;
    [SerializeField] MeshRenderer gus, hand;

    [SerializeField] TutorialManager tutorialManager;
    float waitVideo;
    bool tepeado = false;
    //[SerializeField] tiempo

    [SerializeField] LavaderoPuzzle _lavadero;
    [SerializeField] GameObject _marcoTexto;
    [SerializeField] PuzzleNene _puzzleNene;

    void Start()
    {

        ElectorVideo();


        if (tutorial == false)
        {
            apagado();
            waitVideo = 0;
            videoPlayer.Play();
            tepeado = false;
        }
        else if (tutorial == true)
        {
            waitVideo = 0;
            videoPlayer.Play();
        }
    }


    void Update()
    {

        if (tutorial == false)
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
        else if (tutorial == true)
        {
            GameManager.Instance.Player.UpdateTerrorFrame();
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
        //barra.SetActive(false);
        //gato.SetActive(false);
        //perro.SetActive(false);
        //abuela.SetActive(false);
        negro.enabled = false;
        skipBoton.SetActive(true);
        postit.SetActive(false);
        textoTraps.SetActive(false);
        abuelaDormida.SetActive(false);
        if (boss == true)
        {
            sliderBoss.SetActive(false);
        }

        if(_marcoTexto!=null)
            _marcoTexto.SetActive(false);
    }

    void prendido()
    {
        //postit.SetActive(true);
        abuelaDormida.SetActive(true);
        gus.enabled = true;
        hand.enabled = true;
        marco1.enabled = true;
        marco2.enabled = true;
        marco3.enabled = true;
        vida.enabled = true;
        crosshair.SetActive(true);
        //barra.SetActive(true);
        //gato.SetActive(true);
        //perro.SetActive(true);
        //abuela.SetActive(true);
        videoPlayer.enabled = false;
        skipBoton.SetActive(false);
        textoTraps.SetActive(true);
        //GameManager.Instance.Tutorial.StartPickUp();
        if (boss == true)
        {
            sliderBoss.SetActive(true);
        }

        if (_marcoTexto != null)
            _marcoTexto.SetActive(true);
        if(_lavadero != null)
            _lavadero.ActivarPuzzle();
        if (_puzzleNene != null)
            _puzzleNene.LCDTM();
    }

    void TPGus()
    {
        player.transform.position = entrada.transform.position;
    }

    public void SkipButton()
    {
        waitVideo = 1000;
    }

    void ElectorVideo()
    {
        #region No
        //(Application.dataPath + "/Videos");

        //videosObject = Resources.LoadAll(Application.dataPath + "/Videos");

        //listaTexto = Directory.GetFiles(Application.dataPath + "/Videos", "*.mp4");

        //DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Videos");
        //FileInfo[] info = dir.GetFiles("*.mp4");

        //foreach (FileInfo f in info)
        //{
        //    print(f.Name);
        //    videosLista.Add(f.ConvertTo());
        //} 
        #endregion


        int elegido = Random.Range(0, CatalizadorVideos.videoClips.Length);

        videoPlayer.clip = CatalizadorVideos.videoClips[elegido];
    }
}
