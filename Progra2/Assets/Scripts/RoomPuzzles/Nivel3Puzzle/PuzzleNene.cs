using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaCatolicaPuzzle;
using UnityEditor.VersionControl;

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

    [SerializeField] RoomTrigger[] _nextRooms;
    AINodeManager _nodeManager;

    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();


        SubscribeChildToToy();
        _pendejito.OnChildLaugh += ActivateParent;
        _pendejito.OnChildCry += CompletePuzzle;

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
    }

    void ActivateParent()
    {
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        _parents[_parentIndex].gameObject.SetActive(true);
        _parentIndex++;
        _pendejito.OnChildLaugh -= ActivateParent;
    }
}
