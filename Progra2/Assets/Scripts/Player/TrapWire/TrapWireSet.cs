using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWireSet : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField] bool primeraPuesta = false, segundaPuesta = false;
    [SerializeField] GameObject trapPrefab1, trapPrefab2, visualRef;
    [SerializeField] Transform primerTrapLugar, segundaTrapLugar;
    [SerializeField] LineRenderer linea;
    [SerializeField] LayerMask mask;
    [SerializeField] Transform camCenter;
    [SerializeField] float _maxDistance;
    [SerializeField] CreatePlayerTrap createPlayerTrapScript;
    [SerializeField] Material verde, rojo;

    void Start()
    {
        createPlayerTrapScript = GetComponent<CreatePlayerTrap>();
    }


    void Update()
    {
        //ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawLine(Camera.main.transform.position ,ray.direction, Color.red);

        if (SelectorUI.habAct == 5)
        {
            if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 100, mask) && !hit.transform.GetComponent<RoomTrigger>()) //5dist
            {
                visualRef.transform.position = hit.point;
                Renderer visualRender = visualRef.GetComponent<Renderer>();

                //if (Vector3.SqrMagnitude(transform.position - hit.point) < (/*_maxDistance * _maxDistance*/ 5 * 5))
                if ((Vector3.Distance(transform.position, hit.point)) <= 5.4)
                {
                    visualRender.material = verde;
                }
                else
                {
                    visualRender.material = rojo;
                }

            }

            if (Input.GetKeyUp(KeyCode.F) && !primeraPuesta && createPlayerTrapScript.currentTraps < createPlayerTrapScript.maxTraps)
            {
                SetTrap();
                //primeraPuesta = true;
            }
            else if (Input.GetKeyUp(KeyCode.F) && primeraPuesta && !segundaPuesta)
            {
                SetSecondTrap();
                //segundaPuesta = true;
            }

            if (primeraPuesta && segundaPuesta)
            {
                linea.enabled = true;
                linea.SetPosition(0, primerTrapLugar.position);
                linea.SetPosition(1, segundaTrapLugar.position);
            }
        }
        else
        {
            visualRef.transform.position = new Vector3(1000f, 1000f, 1000f);
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
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 5, mask) && !hit.transform.GetComponent<RoomTrigger>())
        //if (Physics.Raycast(ray, out hit, mask))
        {
            var newFirstTrap = Instantiate(trapPrefab1, hit.point /*new Vector3(hit.point.x, hit.point.y, hit.point.z)*/, Quaternion.identity);
            primerTrapLugar = newFirstTrap.transform; //new Vector3(hit.point.x, hit.point.y, hit.point.z);
            primeraPuesta = true;
        }
    }

    public void SetSecondTrap()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 5, mask) && (Vector3.SqrMagnitude(primerTrapLugar.position - hit.point) < (_maxDistance * _maxDistance)) && !hit.transform.GetComponent<RoomTrigger>())
        //if (Physics.Raycast(ray, out hit, mask))
        {
            var newSecondTrap = Instantiate(trapPrefab2, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            newSecondTrap.gameObject.GetComponent<Trap2>().Initialize(primerTrapLugar);
            segundaTrapLugar = newSecondTrap.transform; //new Vector3(hit.point.x, hit.point.y, hit.point.z);
            segundaPuesta = true;
            createPlayerTrapScript.currentTraps++;
            ResetTrap();
        }
    }

    private void ResetTrap()
    {
        primeraPuesta = false;
        segundaPuesta = false;
        primerTrapLugar = null;
        segundaTrapLugar = null;
    }
}
