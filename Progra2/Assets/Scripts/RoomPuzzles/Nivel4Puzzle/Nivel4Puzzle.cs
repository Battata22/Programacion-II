using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public abstract class Nivel4Puzzle : MonoBehaviour
    {
        [SerializeField] protected HoseOwner _houseOwner;
        [SerializeField] protected Transform _posForHouseOwner;

        abstract public void ActivatePuzzle();
        public virtual Transform GiveDestination()
        {
            return _posForHouseOwner;
        }

        abstract public void KickOutNpc();
    }
}
