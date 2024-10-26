using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour
{
    [SerializeField] KeyCode tecla;
    [SerializeField] GameObject pointerRef;
    [SerializeField] Image fondo, fondoCirc, s1, s2, s3, s4, s5, s6, s7, s8;
    [SerializeField] Canvas tab;
    [SerializeField] bool enTecla = false;

    void Start()
    {
        
    }


    void Update()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enTecla = true;

        //if (collision.gameObject.name == ("1"))
        //{
        //    print("1");
        //}
        //else if (collision.gameObject.name == ("2"))
        //{
        //    print("2");
        //}
        //else if (collision.gameObject.name == ("3"))
        //{
        //    print("3");
        //}
        //else if (collision.gameObject.name == ("4"))
        //{
        //    print("4");
        //}
        //else if (collision.gameObject.name == ("5"))
        //{
        //    print("5");
        //}
        //else if (collision.gameObject.name == ("6"))
        //{
        //    print("6");
        //}
        //else if (collision.gameObject.name == ("7"))
        //{
        //    print("7");
        //}
        //else if (collision.gameObject.name == ("8"))
        //{
        //    print("8");
        //}
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        enTecla = false;
    }
}
