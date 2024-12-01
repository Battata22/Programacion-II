using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour
{
    [SerializeField] KeyCode openMenu, useAbility;
    [SerializeField] GameObject pointerRef;
    [SerializeField] Image fondo, fondoCirc, s1, s2, s3, s4, s5, s6, s7, s8;
    [SerializeField] Canvas tab;
    [SerializeField] bool enTecla = false;
    public static int habAct;
    //public delegate void habManager();

    /// <summary>
    /// ESTO ES UN EVENTO, SOLO USAR += o -=
    /// </summary>   
    public static DelegateType.VoidDelegateTrans habilitiesManager;

    private void Awake()
    {
        habAct = 0;
    }

    void Start()
    {
        habilitiesManager = delegate { }; //Nada;
        habilitiesManager += Test;
    }


    void Update()
    {

        //if (Input.GetKeyDown(useAbility) && habAct !=3)
        //{
        //    habilitiesManager();
        //}

        pointerRef.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f);

        if (Input.GetKey(openMenu))
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
        if (tab.enabled == true) return;

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
        if (tab.enabled == false) return;

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

    public void Test(Transform noSeUsaPeroConfiaQueEstoEsLaMejorManera)
    {
        //print($"<color=#2CD9C9> Llamada Habilidad con F habilidad activa {habAct}</color>");
    }

    //public void Nada()
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    enTecla = true;

    //}



    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    enTecla = false;
    //}
}
