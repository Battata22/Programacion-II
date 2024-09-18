using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesOpciones : MonoBehaviour
{
    [SerializeField] VolumeSettings volumeSettingsScript;
    void Start()
    {
        volumeSettingsScript = GetComponentInParent<VolumeSettings>();
    }

    void Update()
    {

    }

    public void VolverOnClicked()
    {

        SceneManager.LoadScene("Menu");
    }
}
