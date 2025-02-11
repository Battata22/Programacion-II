using CasaFiesta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedorPiso1 : Nivel4Puzzle
{
    //idea, hacer que se pegue un slide
    // si el slide le pega a la mesa de vidrio
    //la rompe y compreta el puzzle

    [SerializeField] Asustable[] _myNpc;
    [SerializeField] Transform _exit;
    [SerializeField] RoomTrigger _myRoom;
    [SerializeField] HouseExitTrigger _houseExit;
    [SerializeField] GlassTable _table;   

    bool _roomCompleted = false;
    private void Awake()
    {
        _myRoom = GetComponent<RoomTrigger>();
    }

    private void Start()
    {
        //ActivatePuzzle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CompletePuzzle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_roomCompleted && other.gameObject == _houseOwner.gameObject)
        {
            Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");

            _houseOwner.StartKickOut(3f);
        }
    }

    void CompletePuzzle()
    {
        #region shit
        //foreach (var npc in _myNpc)
        //{
        //    _houseExit.AddToList(npc.gameObject);
        //    npc.InfiniteScared(true);
        //    npc.GetScared(1f, -1, _exit);
        //}

        //_roomCompleted = true;
        //StartCoroutine(ConstantScare()); 
        #endregion

        _roomCompleted = true;
        _houseOwner.AddToCompleteList(this);
    }

    public override void ActivatePuzzle()
    {
        //Debug.Log($"<color=red> Activar Puzzle no hace nada XDD </color>");

        _table.OnBroken += CompletePuzzle;
    }

    IEnumerator ConstantScare()
    {
        while (_roomCompleted)
        {
            foreach(var npc in _myNpc)
            {
                if (npc.gameObject.activeInHierarchy)
                    npc.GetScared(1f, -1, _exit);
                _roomCompleted = npc.gameObject.activeInHierarchy;
            }

            Debug.Log($"<color=red>Puzzle asustando constantemente</color>");

            yield return new WaitForSeconds(1f);
        }
    }

    public override void KickOutNpc()
    {

        foreach (var npc in _myNpc)
        {
            _houseExit.AddToList(npc.gameObject);
            npc.InfiniteScared(true);
            npc.GetScared(1f, -1, _exit);
        }

        _roomCompleted = true;
        StartCoroutine(ConstantScare());
    }
}
