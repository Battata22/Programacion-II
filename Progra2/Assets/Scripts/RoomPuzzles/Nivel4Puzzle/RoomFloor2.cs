using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CasaFiesta;

public class RoomFloor2 : Nivel4Puzzle
{
    [SerializeField] Asustable[] _myNpc;
    [SerializeField] Transform _exit;
    [SerializeField] RoomTrigger _myRoom;
    [SerializeField] HouseExitTrigger _houseExit;

    private void Awake()
    {
        _myRoom = GetComponent<RoomTrigger>();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        CompletePuzzle();
    //    }
    //}

    void CompletePuzzle()
    {
        foreach (var npc in _myNpc)
        {
            _houseExit.AddToList(npc.gameObject);
            npc.InfiniteScared(true);
            npc.GetScared(1f, -1, _exit);
        }
    }

    public override void ActivatePuzzle()
    {
        Debug.Log($"<color=red> Activar Puzzle no hace nada XDD </color>");
    }

    public override void KickOutNpc()
    {
        throw new System.NotImplementedException();
    }

    protected override void Fireflies()
    {
        throw new System.NotImplementedException();
    }
}
