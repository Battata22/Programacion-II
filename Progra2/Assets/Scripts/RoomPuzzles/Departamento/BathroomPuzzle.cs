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
    [SerializeField] Fireflies _fireFlies;
    [SerializeField] Fireflies _fliesPrefab;
    [SerializeField] Pickable[] _pickables;
    AINodeManager _nodeManager;
    [SerializeField, Tooltip("The Living Puzzle")] LivingPuzzle _nextPuzzle;

    bool _fliesActive = false;
    bool _puzzleActive = true;

    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();
        _bath.ActionActive += CompleteRoom;
    }

    private void Update()
    {
        if (_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;
            var newFlies = Instantiate(_fliesPrefab, _pickables[Random.Range(0, _pickables.Length)].transform.position, Quaternion.identity);
            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }
    void DeactivateFlies()
    {
        _fliesActive = false;
    }

    void CompleteRoom()
    {
        //Debug.Log($"<color=green> CUARTO COMPLETADO </color>");
        _nextPuzzle.StartPuzzle();

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

        _puzzleActive = false;
        _fireFlies.gameObject.SetActive(false);

        GameManager.Instance._master2.ActivarGB();
    }

    private void OnDestroy()
    {
        _bath.ActionActive -= CompleteRoom;
    }
}
