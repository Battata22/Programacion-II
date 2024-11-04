using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectorBotones : MonoBehaviour
{
    [SerializeField] float scale;
    Image imagen;
    int verde, selectedActual;

    void Start()
    {
        imagen = GetComponent<Image>();
    }


    void Update()
    {
        if (SelectorUI.habAct == int.Parse(gameObject.name))
        {
            imagen.color = Color.green;
        }
        else
        {
            imagen.color = Color.gray;
        }

        SelectedAction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        selectedActual = int.Parse(gameObject.name);
    }

    #region Old
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.name == "CursorRef")
    //    {
    //        if (gameObject.name == "1")
    //        {
    //            Opc1();
    //        }
    //        else if (gameObject.name == "2")
    //        {
    //            Opc2();
    //        }
    //        else if (gameObject.name == "3")
    //        {
    //            Opc3();
    //        }
    //        else if (gameObject.name == "4")
    //        {
    //            Opc4();
    //        }
    //        else if (gameObject.name == "5")
    //        {
    //            Opc5();
    //        }
    //        else if (gameObject.name == "6")
    //        {
    //            Opc6();
    //        }
    //        else if (gameObject.name == "7")
    //        {
    //            Opc7();
    //        }
    //        else if (gameObject.name == "8")
    //        {
    //            Opc8();
    //        }
    //    }

    //} 
    #endregion

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.localScale = Vector3.one;

        selectedActual = 0;
    }

    public void SelectedAction()
    {
        if (selectedActual == 1)
        {
            Opc1();
        }
        else if (selectedActual == 2)
        {
            Opc2();
        }
        else if (selectedActual == 3)
        {
            Opc3();
        }
        else if (selectedActual == 4)
        {
            Opc4();
        }
        else if (selectedActual == 5)
        {
            Opc5();
        }
        else if (selectedActual == 6)
        {
            Opc6();
        }
        else if (selectedActual == 7)
        {
            Opc7();
        }
        else if (selectedActual == 8)
        {
            Opc8();
        }
    }

    public void Opc1()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print($"click { gameObject.name } : Tornado");
        }

        SelectorUI.habAct = 1;
    }

    public void Opc2()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print($"click {gameObject.name} : Sombra");
        }

        SelectorUI.habAct = 2;
    }

    public void Opc3()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print($"click {gameObject.name} : Encantar");
        }

        SelectorUI.habAct = 3;
    }

    public void Opc4()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print($"click { gameObject.name} : Moco");
        }

        SelectorUI.habAct = 4;
    }

    public void Opc5()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print($"click {gameObject.name} : TrapWire");
        }

        SelectorUI.habAct = 5;
    }

    public void Opc6()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print("click " + gameObject.name);
        }

        SelectorUI.habAct = 6;
    }

    public void Opc7()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print("click " + gameObject.name);
        }

        SelectorUI.habAct = 7;
    }

    public void Opc8()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (Input.GetMouseButtonUp(0))
        {
            print("click " + gameObject.name);
        }

        SelectorUI.habAct = 8;
    }

}
