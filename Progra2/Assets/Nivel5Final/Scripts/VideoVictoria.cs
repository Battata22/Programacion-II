using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoVictoria : MonoBehaviour
{
    [SerializeField] bool repeat = false;

    public Image marco;
    public List<Sprite> fotos;
    [SerializeField] float vel;
    private int index;
    private bool isDone;

    public static bool victoria = false;

    void Start()
    {
        marco = GetComponent<Image>();
        StartCoroutine(StartAnim());
    }


    void Update()
    {
        if (victoria == true)
        {
            marco.enabled = true;
            StartCoroutine(StartAnim());
            victoria = false;
        }
    }

    IEnumerator StartAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(vel);
            index++;
            if (index >= fotos.Count)
                index = 0;
            else
                marco.sprite = fotos[index];
        }
    }


}
