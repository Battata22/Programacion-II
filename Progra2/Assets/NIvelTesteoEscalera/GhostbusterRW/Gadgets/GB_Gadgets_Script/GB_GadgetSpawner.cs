using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GB_GadgetSpawner : MonoBehaviour
{
    //[SerializeField] GB_Gadget[] gB_Gadgets;
    [SerializeField] GB_GadgetList[] gB_Gadgets;

    [SerializeField] LayerMask _camMask;
    [SerializeField] LayerMask _nodeMask;
    [SerializeField] LayerMask _doorMask;

    [SerializeField] List<GameObject> _objectsDetected = new();

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.LeftAlt))
    //    {
    //        if(Input.GetKeyUp(KeyCode.Alpha1))
    //        {
    //            SpawnGadget(GameManager.Instance.Player.transform, 0);
    //        }
    //        if (Input.GetKeyUp(KeyCode.Alpha2))
    //        {
    //            SpawnGadget(GameManager.Instance.Player.transform, 1);
    //        }
    //    }
    //}

    public void SpawnGadget(Transform pos, int index, Ghostbuster owner = null)
    {
        //Debug.Log($"<color=yellow> Spawneando {gB_Gadgets[index].name} </color>");

        if (index == 1)//1 es el index de la cam;
        {
            if (CheckForCams())
            {
                Debug.Log("<color=green> CAMARA EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
            if (CheckForNodes())
            {
                Debug.Log("<color=magenta> NODO EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
            if (CheckForDoors())
            {
                Debug.Log("<color=yellow> PUERTA EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
        }

        switch (gB_Gadgets[index]._amount)
        {
            case -10:
                //Debug.Log($"<color=yellow>{gB_Gadgets[index].gadget.name} usado, Infinitos restantes </color>");
                break;
            case > 0:
                gB_Gadgets[index]._amount--;
                Debug.Log($"<color=green> {gB_Gadgets[index].gadget.name} usado, quedan {gB_Gadgets[index]._amount} </color>");
                break;
            case <= 0:
                Debug.Log($"<color=red> YA USASTE TODOS LOS {gB_Gadgets[index].gadget.name} </color>");
                return;
        }

        var gadget = Instantiate(gB_Gadgets[index].gadget, pos.position, pos.rotation);

        if (owner != null)
            gadget.Initialize(owner);
    }
    public void SpawnRandomGadget(Transform pos, Ghostbuster owner = null)
    {
        //Debug.Log($"<color=yellow> Spawneando {gB_Gadgets[index].name} </color>");
        var index = Random.Range(1, gB_Gadgets.Length);

        if (index == 1)//1 es el index de la cam;
        {
            if (CheckForCams())
            {
                Debug.Log("<color=green> CAMARA EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
            if (CheckForNodes())
            {
                Debug.Log("<color=magenta> NODO EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
            if (CheckForDoors())
            {
                Debug.Log("<color=yellow> PUERTA EN RANGO</color>,<color=red> Cancelando Spawn </color>");
                return;
            }
        }

        switch (gB_Gadgets[index]._amount)
        {
            case -10:
                //Debug.Log($"<color=yellow>{gB_Gadgets[index].gadget.name} usado, Infinitos restantes </color>");
                break;
            case > 0:
                gB_Gadgets[index]._amount--;
                //Debug.Log($"<color=green> {gB_Gadgets[index].gadget.name} usado, quedan {gB_Gadgets[index]._amount} </color>");
                break;
            case <= 0:
                //Debug.Log($"<color=red> YA USASTE TODOS LOS {gB_Gadgets[index].gadget.name} </color>");
                return;
                break;
        }

        

        var gadget = Instantiate(gB_Gadgets[index].gadget, pos.position, pos.rotation);

        if (owner != null)
            gadget.Initialize(owner);
    }

    /// <summary>
    /// Returns true if cam on range
    /// </summary>
    /// <returns></returns>
    bool CheckForCams()
    {
        bool camOnRange = false;

        var cams = Physics.OverlapSphere(transform.position, 2f, _camMask);

        foreach(var cam in cams)
        {
            if(cam.transform.GetComponent<GB_Cam>())
                camOnRange = true;
        }

        return camOnRange;
    }

    bool CheckForNodes()
    {
        bool toClose = false;

        var nodes = Physics.OverlapSphere(transform.position, 0.5f, _nodeMask);

        foreach(var node in nodes)
        {
            if (node.gameObject.CompareTag("Node"))
            {
                toClose = true;
            }
        }
        return toClose;
    }

    bool CheckForDoors()
    {
        _objectsDetected.Clear();
        bool doorInRange = false;

        var doors = Physics.OverlapSphere(transform.position, 1f, _doorMask);

        foreach(var door in doors)
        {
            if (door.transform.GetComponent<Door>() && !door.isTrigger)
                doorInRange = true;
            _objectsDetected.Add(door.gameObject);
        }

        return doorInRange;
    }

    [System.Serializable] public struct GB_GadgetList
    {
        [Header("<color=yellow> Prefab & amount </color>")]
        public GB_Gadget gadget;

        [SerializeField, Tooltip("Set to -10 for no limits")]
        public int _amount;
    }
}
