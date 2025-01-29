using CasaCatolicaPuzzle;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
    [SerializeField] Door[] _roomDoors;
    [SerializeField] Door[] _finalDoors;
    [SerializeField] Asustable[] _finalNpcs;
    [SerializeField] Exorcista _exorcista;
    [SerializeField] int _currentStage = 0;
    public int CurrentStage {  get { return _currentStage; } }

    AINodeManager _nodeManager;


    private void Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Asustable>() == npcInRoom)
        {
            StartPuzzle();
        }
    }

    public void StartPuzzle()
    {
        _currentStage++;

        npcPuzzle = npcInRoom.transform.AddComponent<NpcCocinaPuzzle>();
        npcPuzzle.Initialize(this);
        npcPuzzle.StartMicrowave();
        microwave.MicroondasPuzzle.active = true;

        LockDoors();
    }

    public void CompletePuzzle()
    {
        Debug.Log("<color=#f0b100>Puzzle completado</color>");
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
        }

        microPuzzle.doCheck = false;

        _exorcista.gameObject.SetActive(true);

        _exorcista.gameObject.SetActive(true);

        npcPuzzle.DrestroyMeBaby();
    }

    void LockDoors()
    {
        foreach (var door in _roomDoors)
        {
            door.LockDoor();
        }
    }
}
