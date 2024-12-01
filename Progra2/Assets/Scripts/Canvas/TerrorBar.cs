using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrorBar : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.terrorBar = this.GetComponent<Slider>();
        Desactivar();
    }

    private void Update()
    {
        if(this != GameManager.Instance.terrorBar)
        {
            //Debug.Log("<color=red> MATENMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE </color>");
        }
    }

    private void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
