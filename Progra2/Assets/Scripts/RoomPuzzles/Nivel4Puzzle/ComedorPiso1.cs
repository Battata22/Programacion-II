using CasaFiesta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedorPiso1 : Nivel4Puzzle
{
    [SerializeField] Asustable[] _myNpc;
    [SerializeField] Transform _exit;
    [SerializeField] RoomTrigger _myRoom;
    [SerializeField] HouseExitTrigger _houseExit;

    //a
    private void Awake()
    {
        _myRoom = GetComponent<RoomTrigger>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        foreach (var npc in _myNpc)
        {
            _houseExit.AddToList(npc.gameObject);
            npc.InfiniteScared(true);
            npc.GetScared(1f, -1, _exit);
        }
    }
}
