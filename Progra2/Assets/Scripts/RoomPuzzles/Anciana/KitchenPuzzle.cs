using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenPuzzle : MonoBehaviour
{
    [SerializeField] bool _watherOn = false;
    [SerializeField] Asustable _granny;
    [SerializeField] Fridge _fridge;
    [SerializeField] CanillaTrigger _canillaTrigger;
    [SerializeField] Door[] _doors;
    [SerializeField] Cat _cat;
    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    private void Start()
    {
        _granny.OnSlideStop += CompleteRoom;
        _nodeManager = GetComponentInParent<AINodeManager>();
        _canillaTrigger.OnCanillaBreak += SetWather;
    }

    void SetWather()
    {
        _watherOn = true;
        _fridge.CreateTrap();
        _canillaTrigger.OnCanillaBreak -= SetWather;
    }

    void CompleteRoom()
    {
        if (!_watherOn) return;
        
        foreach (var door in _doors)
        {
            door.OpenDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        _cat.gameObject.SetActive(false);
        _granny.OnSlideStop -= CompleteRoom;

        GameManager.Instance.ActivateTerrorBar();
    }

    private void OnDestroy()
    {
        _granny.OnSlideStop -= CompleteRoom;
        _canillaTrigger.OnCanillaBreak -= SetWather;

    }
}
