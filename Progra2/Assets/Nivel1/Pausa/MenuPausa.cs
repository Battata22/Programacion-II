using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausaMenu, pausaOpciones;
    public CamRotation camScript;
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
    }

    public void Despausar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camScript.enabled = true;
        paused = false;
        pausaMenu.SetActive(false);
        Time.timeScale = 1;
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
