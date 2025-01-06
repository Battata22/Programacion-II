using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_GadgetSpawner : MonoBehaviour
{
    [SerializeField] GB_Gadget[] gB_Gadgets;

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

        var gadget = Instantiate(gB_Gadgets[index], pos.position, pos.rotation);

        if (owner != null)
            gadget.Initialize(owner);
    }
}
