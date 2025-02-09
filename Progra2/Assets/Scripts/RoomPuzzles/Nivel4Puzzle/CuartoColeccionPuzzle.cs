using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;

public class CuartoColeccionPuzzzle : Nivel4Puzzle
{
    /*
     * idea
     * Cuarto lleno de mierdas coleccionables
     * ir activando las weas
     * despues de romper un par de cosas el dueño de la casa aparece
     * el dueño se enoja y hecha a la mierda al borrachin
    */

    [SerializeField] public Asustable _npcInRoom;
    [SerializeField] List<ObjetoColeccion> _coleccionables = new();
    [SerializeField, Tooltip("X objects broke to complete puzzle")] int hasToBreak;
    int _brokenObjs=0;
    [SerializeField] HouseExitTrigger _houseExit;

    bool completed = false;
    bool llamandoDuenho = false;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        CompletePuzzle();
    //    }
    //}

    public void CompletePuzzle()
    {
        Debug.Log($"<color=#5d14c4>entre a complete</color>");

        if (completed) return;
        Debug.Log($"<color=#5d14c4>pase el if de complete</color>");

        //llamado cuando se rompen X coleccionables
        //llamar duenho
        //hacer que borracho salga de casa

        completed = true;
        _houseOwner.AddToCompleteList(this);

        //completed = true;

        //llamandoDuenho=true;
        //StartCoroutine(LlamarOwner());
        ////_houseOwner.SetSpecificDestination(_npcInRoom.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (completed && other.gameObject == _houseOwner.gameObject)
        {
            //llamandoDuenho = false;
            //Invoke("KickOut", 3f);

            Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");
            _houseOwner.StartKickOut(3f);
        }
    }

    #region comment
    //IEnumerator LlamarOwner()
    //{
    //    var wait = new WaitForSeconds(0.3f);
    //    while (llamandoDuenho)
    //    {
    //        _houseOwner.SetSpecificDestination(_npcInRoom.transform);

    //        yield return wait;
    //    }
    //}

    //void KickOut()
    //{
    //    Debug.Log($"<color=#5d14c4>echando a borracho</color>");

    //    _houseExit.AddToList(_npcInRoom.transform.gameObject);
    //    _npcInRoom.InfiniteScared(true);
    //    _npcInRoom.GetScared(1,-1, _houseExit.transform);
    //} 
    #endregion

    public override void ActivatePuzzle()
    {
        Debug.Log("Huh?");
    }

    public void ObjectBroken()
    {
        Debug.Log($"<color=#5d14c4>Entre a romper</color>");
        _brokenObjs++;
        if(_brokenObjs >= hasToBreak)
            CompletePuzzle();
    }

    public override void KickOutNpc()
    {
        Debug.Log($"<color=#5d14c4>echando a borracho</color>");


        _houseExit.AddToList(_npcInRoom.gameObject);
        _npcInRoom.InfiniteScared(true);
        _npcInRoom.GetScared(1, -1, _houseExit.transform);
    }
}
