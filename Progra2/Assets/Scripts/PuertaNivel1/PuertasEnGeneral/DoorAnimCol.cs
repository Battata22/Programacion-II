using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimCol : MonoBehaviour
{
    //a
    [SerializeField] Door _myDoor;

    public void ActivateCol()
    {
        Debug.Log("Si Me Ves Muchas Veces Llora");
        //_myDoor.SetColider(true);
    }

    public void DeactivateCol()
    {
        Debug.Log("Activate colicion de mierda puta");
        //_myDoor.SetColider(false);
    }
}
