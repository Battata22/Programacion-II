using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesMenu : MonoBehaviour
{

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
        SceneManager.LoadScene("Nivel1");
    }
    public void HTPOnClicked()
    {
        SceneManager.LoadScene("HTP");
    }
    public void NivelesOnClicked()
    {
        SceneManager.LoadScene("Niveles");
    }
    public void OpcionesOnClicked()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void CreditosOnClicked()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void SalirOnClicked()
    {
        Application.Quit();
    }
    public void TestingOnClicked()
    {
        SceneManager.LoadScene("AssetsMuseo");
    }
}
