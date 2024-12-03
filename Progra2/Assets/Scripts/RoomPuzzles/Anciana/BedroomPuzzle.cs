using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomPuzzle : MonoBehaviour
{
    [SerializeField] Key _keyPrefab;
    [SerializeField] Asustable _granny;
    [SerializeField] Closet _closet;
    [SerializeField] Door[] _doors;
    [SerializeField] GameObject[] _pets;
    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    [SerializeField] CanillaTrigger _canillaTrigger;

    [SerializeField] Transform keyRescue;

    private void Start()
    {
        _granny.OnRagdollTrigger += SpawnKey;
        _nodeManager = GetComponentInParent<AINodeManager>();
    }

    void SpawnKey()
    {
        Instantiate(_keyPrefab, _granny.transform.position, Quaternion.identity);
        _granny.OnRagdollTrigger -= SpawnKey;
        _closet.ActionActive += CompleteRoom;
    }

    void CompleteRoom()
    {
        Debug.Log($"<color=green> CUARTO COMPLETADO </color>");

        foreach(var door in _doors)
        {
            door.OpenDoor();
        }

        bool resetList = true;
        foreach(var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        foreach(var pet in _pets)
        {
            pet.SetActive(true);
        }
        _closet.ActionActive -= CompleteRoom;

        //GameManager.Instance.Master1.ActivarGB();
        _canillaTrigger.CallShit();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Key>(out var key))
        {
            key.transform.position = keyRescue.position + new Vector3(0,1,0);
        }
    }

    private void OnDestroy()
    {
        _granny.OnRagdollTrigger -= SpawnKey;
        _closet.ActionActive -= CompleteRoom;

    }
}
