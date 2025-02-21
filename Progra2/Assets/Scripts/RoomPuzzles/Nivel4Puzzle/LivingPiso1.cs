using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{

    public class LivingPiso1 : Nivel4Puzzle
    {
        [SerializeField] HouseExitTrigger _houseExit;
        [SerializeField] Asustable _npcInRoom;
        [SerializeField] DiscoBall _discoBall;

        bool _active = false;
        bool completed = false;

        private void Start()
        {
            _discoBall.OnBreak += CompletePuzzle;
        }

        public override void ActivatePuzzle()
        {
            Debug.Log("Huh?");

            _active = true;
            _discoBall.CanGetDamage(true);

            ChangeText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_active && other.gameObject.GetComponent<Player>())
                ChangeText();

            if (completed && other.gameObject == _houseOwner.gameObject)
            {
                Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");

                _houseOwner.StartKickOut(3f);
            }
        }

        public void CompletePuzzle()
        {
            _discoBall.OnBreak -= CompletePuzzle;
            Debug.Log($"<color=#5d14c4>entre a complete</color>");

            if (completed) return;
            Debug.Log($"<color=#5d14c4>pase el if de complete</color>");

            completed = true;
            _houseOwner.AddToCompleteList(this);

            _textIndex =1;
            if (GameManager.Instance.Player.actualRoom == GetComponent<RoomTrigger>().roomIndex)
                ChangeText();
        }

        public override void KickOutNpc()
        {
            Debug.Log($"<color=#5d14c4>echando a borracho</color>");


            _houseExit.AddToList(_npcInRoom.gameObject);
            _npcInRoom.InfiniteScared(true);
            _npcInRoom.GetScared(1, -1, _houseExit.transform);
        }
    }
}
