using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour
{
    [SerializeField] KeyCode tecla, habilidad;
    [SerializeField] GameObject pointerRef;
    [SerializeField] Image fondo, fondoCirc, s1, s2, s3, s4, s5, s6, s7, s8;
    [SerializeField] Canvas tab;
    [SerializeField] bool enTecla = false;
    public static int habAct;
    public delegate void habManager();
    public static event habManager habilitiesManager;

    void Start()
    {
        habilitiesManager += Nada;
    }


    void Update()
    {

        if (Input.GetKeyDown(habilidad))
        {
            habilitiesManager();
        }

        pointerRef.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f);

        if (Input.GetKey(tecla))
        {

            SelectorActivo();

        }
        else
        {

            SelectorDesactivado();

        }
    }

    public void SelectorActivo()
    {

        pointerRef.SetActive(true);

        tab.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        #region Comment
        //fondo.enabled = true;
        //s1.enabled = true;
        //s2.enabled = true;
        //s3.enabled = true;
        //s4.enabled = true;
        //s5.enabled = true;
        //s6.enabled = true;
        //s7.enabled = true;
        //s8.enabled = true; 
        #endregion
    }

    public void SelectorDesactivado()
    {

        pointerRef.SetActive(false);

        tab.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        #region Comment
        //fondo.enabled = false;
        //s1.enabled = false;
        //s2.enabled = false;
        //s3.enabled = false;
        //s4.enabled = false;
        //s5.enabled = false;
        //s6.enabled = false;
        //s7.enabled = false;
        //s8.enabled = false; 
        #endregion
    }

    public void test()
    {
        print("encima");
    }

    public void Nada()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    enTecla = true;

    //}



    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    enTecla = false;
    //}
}
