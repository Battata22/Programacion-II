using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaCatolicaPuzzle;

public class PuzzleNene : Nivel3Puzzle
{
    //idea
    //Se despierta el nene
    //Hay que hacerlo reir al lado del monitor de pendejos
    //Entra el padre a revisar
    //asusstar al padre al lado del pendejo
    //pendejo llora
    //padre itena calmarlo
    //madre se despuerta
    //prepara una chocolatada para el pendejo
    //arranca puzzle cocina

    [SerializeField] ChildScript _pendejito;
    [SerializeField] Asustable[] _parents;
    int _parentIndex = 0;
    [SerializeField] SFXToy[] _toys;
    [SerializeField] Door[] _doors;
    [SerializeField] Door _childRoomDoor;

    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    [SerializeField] PuzzleLvl3Cocina _nextPuzzle;

    //Fireflies
    [Header("<color=green>Fireflies</color>")]
    [SerializeField] Fireflies _fliesPrefab;
    bool _puzzleActive = true;
    bool _fliesActive = false;


    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();


        SubscribeChildToToy();
        _pendejito.OnChildLaugh += ActivateParent;
        _pendejito.OnChildCry += CompletePuzzle;

    }

    private void Update()
    {
        if (_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;

            int index = Random.Range(0, _toys.Length);

            var newFlies = Instantiate(_fliesPrefab, _toys[index].transform.position, Quaternion.identity);

            newFlies.SetFocusObj(_toys[index].transform);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }

    void DeactivateFlies()
    {
        _fliesActive = false;
    }

    //lo hago aca porque tengo retriso
    void SubscribeChildToToy()
    {
        foreach(var toy in _toys)
        {
            toy.OnSoundPlay += _pendejito.GoToToy;
        }
    }

    void CompletePuzzle()
    {
        foreach(var door in _doors)
        {
            door.UnlockDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }


        foreach(var parent in _parents)
        {
            parent.gameObject.SetActive(true);
            parent.StartUseOwnNode();
        }

        _pendejito.StartUseOwnNode();

        _puzzleActive = false;
        //_nextPuzzle.StartPuzzle();
    }

    void ActivateParent()
    {
        //  Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");

        _parents[_parentIndex].gameObject.SetActive(true);
        _parentIndex++;

        _childRoomDoor.OnDoorClose += LockChildDoor;
        _childRoomDoor.UnlockDoor();
        //_doors[0].UnlockDoor();

        _pendejito.OnChildLaugh -= ActivateParent;
    }

    void LockChildDoor()
    {
        _childRoomDoor.OnDoorClose -= LockChildDoor;
        _childRoomDoor.LockDoor();
    }
}
