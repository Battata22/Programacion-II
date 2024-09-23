using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesOpciones : MonoBehaviour
{
    [SerializeField] SlidersSettings volumeSettingsScript;
    void Start()
    {
        volumeSettingsScript = GetComponentInParent<SlidersSettings>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            SceneManager.LoadScene("Menu");
        }
    }

    public void VolverOnClicked()
    {

        SceneManager.LoadScene("Menu");
    }
}
