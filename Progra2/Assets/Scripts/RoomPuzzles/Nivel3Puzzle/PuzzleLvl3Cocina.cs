using CasaCatolicaPuzzle;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleLvl3Cocina : Nivel3Puzzle
{
    //idea vieja
    //bloquear puertas de la cocina cuando entre el adulto
    //el npc se pone a preparar algo para calmar al pendejo
    //hay que hacer que eso salga mal, como?
    //calentando extra la leche
    //hacer que la tire
    //Hacer que el horno tire fogo?

    //idea
    //npc se acerca a calentar la lechita
    //despues de un rato la agarra
    //player puede pasar de rosca el microndas
    //si npc agarra se quema/ asusta / tira la merda

    //despues
    //hace la leche en algo fragil
    //si le pegas un slide o ragdoll deja caer la segunda leche

    //tercer shit
    //agarra unas galletas y se las lleva al pendejo

    //hacer scrip por separado para jugar con la ia del npc
    [SerializeField] public Asustable npcInRoom;
    [SerializeField] public NpcCocinaPuzzle npcPuzzle;
    [SerializeField] public SFXMicroondas microwave;
    [SerializeField] public MicroondasPuzzle microPuzzle;
    [SerializeField] public FridgePuzzle _fridge;

    [SerializeField] RoomTrigger[] _nextRooms;
    [SerializeField] ComedorPuzzle _nextPuzzle;
    [SerializeField] Door[] _roomDoors;
    [SerializeField] Door[] _finalDoors;
    [SerializeField] Asustable[] _finalNpcs;
    [SerializeField] Exorcista _exorcista;
    [SerializeField] int _currentStage = 0;
    public int CurrentStage {  get { return _currentStage; } }

    AINodeManager _nodeManager;

    bool _roomComplete = false;

    [SerializeField] Chocotorta _chotocorta;
    [SerializeField] Transform _npcHando;

    //Fireflies
    [Header("<color=green>Fireflies</color>")]
    [SerializeField] Fireflies _fliesPrefab;
    [SerializeField] bool _puzzleActive = false;
    bool _fliesActive = false;

    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();

    }

    private void Update()
    {
        if (_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;

            Fireflies newFlies;

            switch(_currentStage)
            {
                case 1:
                    newFlies = Instantiate(_fliesPrefab, microwave.transform.position, Quaternion.identity);
                    newFlies.SetFocusObj(microwave.transform);
                    break;
                default:
                    newFlies = Instantiate(_fliesPrefab, npcInRoom.transform.position, Quaternion.identity);
                    newFlies.SetFocusObj(npcInRoom.transform);
                    break;
            }
            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }

    void DeactivateFlies()
    {
        _fliesActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Asustable>() == npcInRoom && !_roomComplete)
        {
            StartPuzzle();
        }
    }

    public void StartPuzzle()
    {
        _currentStage++;

        npcPuzzle = npcInRoom.transform.AddComponent<NpcCocinaPuzzle>();
        npcPuzzle.Initialize(this, _chotocorta, _npcHando);
        npcPuzzle.StartMicrowave();
        microwave.MicroondasPuzzle.active = true;

        _puzzleActive = true;
        LockDoors();

        _textIndex = 0;
        ChangeText();
    }
    
    /*
    al completar el puzzle
    el gil en la cocina sale corriendo
    ell que esta con el niño lo deja en el cuarto de los adultos
    el exorcista entra

    */
    public void CompletePuzzle()
    {
        Debug.Log("<color=#f0b100>Puzzle completado</color>");
        _roomComplete = true;

        npcInRoom.OnSlideStop -= CompletePuzzle;
        npcInRoom.OnRagdollEnd -= CompletePuzzle;

        foreach (var door in _finalDoors)
        {
            door.UnlockDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        foreach(var npc in _finalNpcs)
        {
            npc.StopUseOwnNode();
            npc.SetNewDestination();
        }

        microPuzzle.doCheck = false;

        _exorcista.gameObject.SetActive(true);

        _exorcista.gameObject.SetActive(true);

        npcPuzzle.DrestroyMeBaby();

        _nextPuzzle.StartPuzzle();
        _puzzleActive = false;

        _textIndex = _objectiveTexts.Length-1;
        ChangeText();
    }

    public void ChangeState()
    {
        _currentStage++;

        _textIndex = 1;
        ChangeText();
    }

    void LockDoors()
    {
        if(_roomComplete) return;
        foreach (var door in _roomDoors)
        {
            door.LockDoor();
        }
    }
}
