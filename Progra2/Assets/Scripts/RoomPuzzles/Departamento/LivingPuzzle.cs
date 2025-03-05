using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivingPuzzle : MonoBehaviour
{
    [SerializeField] Bath _bath;
    [SerializeField] Door[] _doors;
    [SerializeField] GameObject[] _pets;
    [SerializeField] RoomTrigger[] _nextRooms;
    [SerializeField] Asustable[] asustables;
    AINodeManager _nodeManager;
    [SerializeField] Fireflies _fireFlies;
    [SerializeField] Fireflies _fliesPrefab;
    [SerializeField] List<Sahumerio> _sahumerios = new();

    [SerializeField] DepaRoomPuzzle _roomPuzzle;

    bool _fliesActive = false;
    bool _puzzleActive = false;

    private IEnumerator Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();

        yield return new WaitForEndOfFrame();
        GameManager.Instance.Rociadores.OnRociadoresActive += CompleteRoom;
    }

    private void Update()
    {
        if (_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;

            var target = _sahumerios[Random.Range(0, _sahumerios.Count)].transform;

            var newFlies = Instantiate(_fliesPrefab,target.position, Quaternion.identity);
            newFlies.SetFocusObj(target);
            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }

        TutorialUpdate();
    }

    void DeactivateFlies() 
    {
        _fliesActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Sahumerio>(out var sahumerio))
        {
            AddSahumerio(sahumerio);
        }
    }

    public void StartPuzzle()
    {
        _puzzleActive = true;
        _fireFlies.gameObject.SetActive(true);
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

        var myIndex = transform.GetComponent<RoomTrigger>().roomIndex;
        foreach (var asus in asustables)
        {
            asus.GetScared(1, myIndex);
        }
        GameManager.Instance.Rociadores.OnRociadoresActive -= CompleteRoom;

        _roomPuzzle.Shit();
        _fireFlies.gameObject.SetActive(false);

        GameManager.Instance.ActivateTerrorBar();

        _puzzleActive = false;

        Tutorializador();

    }

    void AddSahumerio(Sahumerio newSahumerio)
    {
        bool addToList = true;
        foreach(var sahumerios in _sahumerios)
        {
            if(sahumerios == newSahumerio)
                addToList = false;
        }

        if(addToList)
            _sahumerios.Add(newSahumerio);
    }
    private void OnDestroy()
    {
        GameManager.Instance.Rociadores.OnRociadoresActive -= CompleteRoom;
    }

    [SerializeField] GameObject _marcoText;
    [SerializeField] TMP_Text _tutorial;

    event DelegateType.VoidDelegate TutorialUpdate = delegate { };
    void Tutorializador()
    {
        _marcoText.SetActive(true);

        _tutorial.text = "'Tab' para menu de poderes \n 'F' para usar poder";

        TutorialUpdate += TutoUpdate;
    }

    bool tabUsed = false;
    void TutoUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            tabUsed = true; ;

        if (tabUsed && Input.GetKeyDown(KeyCode.F))
        {
            TutorialUpdate -= TutoUpdate;

            _tutorial.text = "";
            _marcoText.SetActive(false);
        }

    }
}
