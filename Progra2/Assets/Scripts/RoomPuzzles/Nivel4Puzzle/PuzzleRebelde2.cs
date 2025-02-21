using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    public class PuzzleRebelde2 : MonoBehaviour
    {

        [SerializeField] PuzzleRebelde _ogPuzzle;

        private void Start()
        {
            _ogPuzzle._otraHabitacion = this.GetComponent<RoomTrigger>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _ogPuzzle.SegundoTrigger(other);

            if(other.transform.TryGetComponent<StinkBomb>(out var bomb))
            {
                bomb.OnExplode += ExplodeInRoom;
            }
            if (_ogPuzzle.CheckComplete(other.gameObject))
            {
                Debug.Log($"<color=red>{name} detecte al HouseOwner {other.name}</color>");

                other.gameObject.GetComponent<HoseOwner>().StartKickOut(3f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.TryGetComponent<StinkBomb>(out var bomb))
            {
                bomb.OnExplode -= ExplodeInRoom;
            }
        }

        void ExplodeInRoom()
        {
            Debug.Log("<color=#6e5810> Conchetumare que olor a culo</color>");

            _ogPuzzle.CompletePuzzle();
        }
    }
}
