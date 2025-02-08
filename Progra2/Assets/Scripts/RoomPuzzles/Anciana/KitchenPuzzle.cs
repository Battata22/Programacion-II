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
    [SerializeField] Fireflies _fireflies;
    [SerializeField] Fireflies _fliesPrefab;
    [SerializeField] Pickable[] _midObjs;
    AINodeManager _nodeManager;

    bool _fliesActive = false;
    bool _puzzleActive = false;
    private void Start()
    {
        _granny.OnSlideStop += CompleteRoom;
        _nodeManager = GetComponentInParent<AINodeManager>();
        _canillaTrigger.OnCanillaBreak += SetWather;
    }

    private void Update()
    {
        if(_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;
            int index = Random.Range(0, _midObjs.Length);
            var newFlies = Instantiate(_fliesPrefab, _midObjs[index].transform.position, Quaternion.identity);
            newFlies.SetFocusObj(_midObjs[index].transform);
            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }

    public void StartPuzzle()
    {
        _puzzleActive = true;
        _fireflies.gameObject.SetActive(true);
    }

    void DeactivateFlies()
    {
        _fliesActive = false;
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
            door.UnlockDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        //_cat.gameObject.SetActive(false);
        _granny.OnSlideStop -= CompleteRoom;

        _fireflies.gameObject.SetActive(false);

        GameManager.Instance.ActivateTerrorBar();
        GameManager.Instance.Master1.ActivarGB();
        _canillaTrigger.EndCallEvent();
    }

    private void OnDestroy()
    {
        _granny.OnSlideStop -= CompleteRoom;
        _canillaTrigger.OnCanillaBreak -= SetWather;

    }
}
