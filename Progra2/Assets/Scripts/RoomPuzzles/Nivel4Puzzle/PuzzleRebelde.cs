using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;

namespace CasaFiesta
{
    public class PuzzleRebelde : Nivel4Puzzle
    {
        [Header("<color=green>Special</color>")]
        [SerializeField] Asustable[] _myNpc;
        [SerializeField] Door[] _doors;
        [SerializeField] HouseExitTrigger _houseExit;

        bool _roomCompleted = false;

        public void CompletePuzzle()
        {
            _roomCompleted = true;
            _houseOwner.AddToCompleteList(this);
        }

        public override void ActivatePuzzle()
        {
            throw new System.NotImplementedException();
        }

        public override void KickOutNpc()
        {
            foreach (var npc in _myNpc)
            {
                _houseExit.AddToList(npc.gameObject);
                npc.InfiniteScared(true);
                npc.GetScared(1f, -1, _houseExit.transform);
            }

            _roomCompleted = true;
            StartCoroutine(ConstantScare());
        }

        IEnumerator ConstantScare()
        {
            while (_roomCompleted)
            {
                foreach (var npc in _myNpc)
                {
                    if (npc.gameObject.activeInHierarchy)
                        npc.GetScared(1f, -1, _houseExit.transform);
                    _roomCompleted = npc.gameObject.activeInHierarchy;
                }

                Debug.Log($"<color=red>Puzzle asustando constantemente</color>");

                yield return new WaitForSeconds(1f);
            }
        }

        public bool CheckComplete(GameObject newObj)
        {
            Debug.Log($"<color=green>Entre al check {_roomCompleted}</color>");
            if(_roomCompleted && newObj == _houseOwner.gameObject)
            {
                Debug.Log("<color=green>SALTE DE AQUI PERRO</color>");

                return true;
            }
            Debug.Log($"<color=green>Despues del if {_roomCompleted}</color>");

            return false;
        }
    }
}