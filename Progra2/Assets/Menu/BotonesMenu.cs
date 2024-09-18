using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesMenu : MonoBehaviour
{

    //[SerializeField] Button playBoton, nivelesBoton, opcionesBoton, creditosBoton, salirBoton;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            SceneManager.LoadScene("Victoria");
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            SceneManager.LoadScene("Derrota");
        }
    }

    public void PlayOnClicked()
    {
        print("Play click");
        SceneManager.LoadScene("Nivel1");
    }
    public void NivelesOnClicked()
    {
        print("Niveles click");
        SceneManager.LoadScene("Niveles");
    }
    public void OpcionesOnClicked()
    {
        print("Opciones click");
        SceneManager.LoadScene("Opciones");
    }
    public void CreditosOnClicked()
    {
        print("Creditos click");
        SceneManager.LoadScene("Creditos");
    }
    public void SalirOnClicked()
    {
        print("Salir click");
        Application.Quit();
    }
    public void TestingOnClicked()
    {
        print("Testing click");
        SceneManager.LoadScene("AssetsMuseo");
    }
}
