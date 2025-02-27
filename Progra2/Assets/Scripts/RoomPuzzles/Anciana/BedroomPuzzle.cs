using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomPuzzle : MonoBehaviour
{
    [SerializeField] Key _keyPrefab;
    [SerializeField] Fireflies _fliesPrefab;
    [SerializeField] Asustable _granny;
    [SerializeField] Closet _closet;
    [SerializeField] Door[] _doors;
    [SerializeField] GameObject[] _pets;
    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    [SerializeField] CanillaTrigger _canillaTrigger;

    [SerializeField] Transform keyRescue;

    [SerializeField] List<Fireflies> _allFlies = new();
    [SerializeField, Tooltip("The Kitchen Puzzle")] KitchenPuzzle _nextPuzzle;

    List<GameObject> _destroyList = new();

    private void Start()
    {
        _granny.OnRagdollTrigger += SpawnKey;
        _nodeManager = GetComponentInParent<AINodeManager>();
    }

    void SpawnKey()
    {
        var newKey = Instantiate(_keyPrefab, _granny.transform.position, Quaternion.identity);

        var newFlies = Instantiate(_fliesPrefab, newKey.transform.position, Quaternion.identity);
        newFlies.SetFocusObj(newKey.transform);

        _destroyList.Add(newFlies.gameObject);

        _granny.OnRagdollTrigger -= SpawnKey;
        _closet.ActionActive += CompleteRoom;
    }

    void CompleteRoom()
    {
        Debug.Log($"<color=green> CUARTO COMPLETADO </color>");

        foreach(var flies in _allFlies)
        {
            flies.gameObject.SetActive(false);
        }

        foreach(var door in _doors)
        {
            door.UnlockDoor();
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

        foreach (var destro in _destroyList)
            Destroy(destro);

        //GameManager.Instance.Master1.ActivarGB();
        _nextPuzzle.StartPuzzle();
        _canillaTrigger.CallShit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fireflies>(out var fireflies) && fireflies._state == Fireflies.State.idle)
        {
            AddFlies(fireflies);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Key>(out var key))
        {
            key.transform.position = keyRescue.position + new Vector3(0,1,0);
        }        
    }

    void AddFlies(Fireflies newFlies)
    {
        bool addToList = true;
        foreach(var fly in _allFlies)
        {
            if(fly == newFlies)
                addToList = false;
        }

        if (addToList)
        {
            Debug.Log($"<color=green>{newFlies.name} Añadido a la lista</color>");
            _allFlies.Add(newFlies);
            newFlies.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        _granny.OnRagdollTrigger -= SpawnKey;
        _closet.ActionActive -= CompleteRoom;

    }
}
