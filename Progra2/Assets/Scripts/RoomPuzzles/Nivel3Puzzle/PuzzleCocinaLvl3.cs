using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaCatolicaPuzzle;

namespace NoUsarNuncaEnTuVida
{
    public class PuzzleCocinaLvl3 : Nivel3Puzzle
    {
        //idea
        //bloquear puertas de la cocina cuando entre el adulto
        //el npc se pone a preparar algo para calmar al pendejo
        //hay que hacer que eso salga mal, como?
        //calentando extra la leche
        //hacer que la tire
        //Hacer que el horno tire fogo?


        //[SerializeField] Asustable _npcInRoom;
        //[SerializeField] Door[] _roomDoors;
        //[SerializeField] Door[] _finalDoors;
        //[SerializeField] Exorcista _exorcista;
        //[SerializeField] RoomTrigger[] _nextRooms;
        //AINodeManager _nodeManager;


        //private void Start()
        //{
        //    _nodeManager = GetComponentInParent<AINodeManager>();

        //}

        //void CompletePuzzle()
        //{
        //    foreach(var door in _finalDoors)
        //    {
        //        door.UnlockDoor();
        //    }

        //    bool resetList = true;
        //    foreach (var room in _nextRooms)
        //    {
        //        _nodeManager.SetActiveNodes(room.roomIndex, resetList);
        //        resetList = false;
        //    }

        //    _exorcista.gameObject.SetActive(true);
        //}

        //void LockDoors()
        //{
        //    foreach(var door in _roomDoors)
        //    {
        //        door.LockDoor();
        //    }
        //}

    }
}
