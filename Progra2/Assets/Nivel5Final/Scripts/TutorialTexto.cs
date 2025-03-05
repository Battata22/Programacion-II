using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTexto : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCanvas;
    [SerializeField] List<string> textoList = new List<string>();
    public static int pasoActual = 0;

    void Start()
    {
        pasoActual = 0;
    }


    void Update()
    {
        textoCanvas.text = textoList[pasoActual];
    }
}
