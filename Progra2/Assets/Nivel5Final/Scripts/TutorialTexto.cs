using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTexto : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCanvas;
    [SerializeField] List<string> textoList = new List<string>();
    [SerializeField] Image marco;
    public static int pasoActual = 0;

    void Start()
    {
        pasoActual = 0;
        marco.enabled = false;
        textoCanvas.enabled = false;
    }


    void Update()
    {
        if (VideoIntro.terminoElVideo == true && textoCanvas.enabled == false)
        {
            marco.enabled = true;
            textoCanvas.enabled = true;
        }

        textoCanvas.text = textoList[pasoActual];
    }
}
