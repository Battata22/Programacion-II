using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_GadgetSpawner : MonoBehaviour
{
    //[SerializeField] GB_Gadget[] gB_Gadgets;
    [SerializeField] GB_GadgetList[] gB_Gadgets;

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

        switch (gB_Gadgets[index]._amount)
        {
            case -10:
                Debug.Log($"<color=yellow>{gB_Gadgets[index].gadget.name} usado, Infinitos restantes </color>");
                break;
            case > 0:
                gB_Gadgets[index]._amount--;
                Debug.Log($"<color=green> {gB_Gadgets[index].gadget.name} usado, quedan {gB_Gadgets[index]._amount} </color>");
                break;
            case <= 0:
                Debug.Log($"<color=red> YA USASTE TODOS LOS {gB_Gadgets[index].gadget.name} </color>");
                return;
                break;
        }

        var gadget = Instantiate(gB_Gadgets[index].gadget, pos.position, pos.rotation);

        if (owner != null)
            gadget.Initialize(owner);
    }
    public void SpawnRandomGadget(Transform pos, Ghostbuster owner = null)
    {
        //Debug.Log($"<color=yellow> Spawneando {gB_Gadgets[index].name} </color>");
        var index = Random.Range(1, gB_Gadgets.Length);

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

    [System.Serializable] public struct GB_GadgetList
    {
        [Header("<color=yellow> Prefab & amount </color>")]
        public GB_Gadget gadget;

        [SerializeField, Tooltip("Set to -10 for no limits")]
        public int _amount;
    }
}
