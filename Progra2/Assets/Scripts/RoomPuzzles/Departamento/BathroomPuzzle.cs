using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BathroomPuzzle : MonoBehaviour
{
    // crear vapor
    // activar espejo
    // activar bañera
    // fin

    [SerializeField] Bath _bath;
    [SerializeField] Door[] _doors;
    [SerializeField] GameObject[] _pets;
    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();
        _bath.ActionActive += CompleteRoom;
    }

    void CompleteRoom()
    {
        //Debug.Log($"<color=green> CUARTO COMPLETADO </color>");

        foreach (var door in _doors)
        {
            door.UnlockDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        foreach (var pet in _pets)
        {
            pet.SetActive(true);
        }
        _bath.ActionActive -= CompleteRoom;

        GameManager.Instance._master2.ActivarGB();
    }

    private void OnDestroy()
    {
        _bath.ActionActive -= CompleteRoom;
    }
}
