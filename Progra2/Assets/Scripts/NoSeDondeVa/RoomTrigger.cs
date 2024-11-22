using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] int _thisRoom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IRoomDetectable>(out var coso))
        {
            coso.SetRoom(_thisRoom);
        }
    }
}
