using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWireSet : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField] bool primeraPuesta = false, segundaPuesta = false;
    [SerializeField] GameObject trapPrefab1, trapPrefab2;
    [SerializeField] Vector3 primerTrapLugar, segundaTrapLugar;
    [SerializeField] LineRenderer linea;

    void Start()
    {

    }


    void Update()
    {
        if (SelectorUI.habAct == 5)
        {
            if (Input.GetKeyUp(KeyCode.F) && !primeraPuesta)
            {
                SetTrap();
                primeraPuesta = true;
            }
            else if (Input.GetKeyUp(KeyCode.F) && primeraPuesta && !segundaPuesta)
            {
                SetSecondTrap();
                segundaPuesta = true;
            }

            if (primeraPuesta && segundaPuesta)
            {
                linea.enabled = true;
                linea.SetPosition(0, primerTrapLugar);
                linea.SetPosition(1, segundaTrapLugar);
            }
        }

        //if (SelectorUI.habAct == 4 && !puesto)
        //{
        //    SelectorUI.habilitiesManager += SetTrap;
        //    puesto = true;
        //}
        //else if (SelectorUI.habAct == 4 && puesto)
        //{

        //}
        //else
        //{
        //    SelectorUI.habilitiesManager -= SetTrap;
        //    puesto = false;
        //}
    }

    public void SetTrap()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(trapPrefab1, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            primerTrapLugar = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    public void SetSecondTrap()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var newSecondTrap = Instantiate(trapPrefab2, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            newSecondTrap.gameObject.GetComponent<Trap2>().Initialize(primerTrapLugar);
            segundaTrapLugar = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

}
