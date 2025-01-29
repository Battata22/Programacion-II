using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaCatolicaPuzzle
{
    public class FridgePuzzle : MonoBehaviour
    {
        [Header("<color=#00ffff>Solo para lvl3</color>")]
        [SerializeField] PuzzleLvl3Cocina _myPuzzle;
        [SerializeField] public bool doCheck = false;
        [SerializeField] float checkDist;
        [SerializeField] public bool active = false;

        public event DelegateType.VoidDelegate DaleElChocolate = delegate { };

        private void Update()
        {
            if (doCheck && Vector3.SqrMagnitude(_myPuzzle.npcPuzzle.transform.position - transform.position) < (checkDist * checkDist))
            {
                DameUnPocoDeEseChocolate();
            }
        }

        void DameUnPocoDeEseChocolate()
        {
            doCheck = false;
            //npc agarra postre
            Debug.Log("<color=#a1670b>Pero tenía tantas ganas de comerme el chocolate</color>");
            DaleElChocolate();
            _myPuzzle.npcInRoom.SetNewDestination();
        }

        public void PrenderHeladera()
        {
            active = true;
            doCheck = true;
        }
    }
}
