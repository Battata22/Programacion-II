using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausaMenu, pausaOpciones;
    [SerializeField] VideoPlayer videoPlayer;
    public CamRotation camScript;
    [SerializeField] AudioSource meado;
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<AudioSource> sonando;
    [SerializeField] VideoIntro videoIntroScript;
    bool paused = false, inOptions = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false && videoIntroScript.videoPlayer.isPlaying == false)
            {
                Pausar();
            }
            else if (paused == true && inOptions == false)
            {
                Despausar();
            }
        }
    }

    public void Pausar()
    {
        paused = true;
        pausaMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        camScript.enabled = false;

        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            videoPlayer.Pause();
            if (meado.isPlaying == true)
            {
                meado.Pause();
            }
        }


        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying == true)
            {
                audioSources[i].Pause();
                sonando.Add(audioSources[i]);
            }

        }
    }

    public void Despausar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camScript.enabled = true;
        paused = false;
        pausaMenu.SetActive(false);
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            videoPlayer.Play();
            if (meado.isPlaying == false)
            {
                meado.Play();
            }
        }


        for (int i = 0; i < sonando.Count; i++)
        {
            sonando[i].Play();
        }
        sonando.Clear();

    }

    public void MenuOpciones()
    {
        inOptions = true;
        pausaMenu.SetActive(false);
        pausaOpciones.SetActive(true);
    }

    public void SalirMenuOpciones()
    {
        inOptions = false;
        pausaMenu.SetActive(true);
        pausaOpciones.SetActive(false);
    }

    public void VolverMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }


}
