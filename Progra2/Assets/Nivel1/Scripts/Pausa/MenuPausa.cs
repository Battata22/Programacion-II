using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausaMenu, pausaOpciones;
    public CamRotation camScript;
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<AudioSource> sonando;
    bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Pausar();
            }
            else if (paused == true)
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
        for(int i = 0; i < audioSources.Count; i++)
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
        for (int i = 0; i < sonando.Count; i++)
        {
            sonando[i].Play();
        }
        sonando.Clear();
    }

    public void MenuOpciones()
    {
        pausaMenu.SetActive(false);
        pausaOpciones.SetActive(true);
    }

    public void SalirMenuOpciones()
    {
        pausaMenu.SetActive(true);
        pausaOpciones.SetActive(false);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}