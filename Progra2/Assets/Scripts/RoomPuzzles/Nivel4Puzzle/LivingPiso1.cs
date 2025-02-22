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

        

        //bool _active = false;
        //bool _completed = false;
        //bool _fliesActive = false;

        private void Start()
        {
            _discoBall.OnBreak += CompletePuzzle;
        }

        public override void ActivatePuzzle()
        {
            //Debug.Log("Huh?");

            _active = true;
            _discoBall.CanGetDamage(true);

            ChangeText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_active && other.gameObject.GetComponent<Player>())
                ChangeText();

            if (_completed && other.gameObject == _houseOwner.gameObject)
            {
                Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");

                _houseOwner.StartKickOut(3f);
            }
        }

        public void CompletePuzzle()
        {
            _discoBall.OnBreak -= CompletePuzzle;
            Debug.Log($"<color=#5d14c4>entre a complete</color>");

            if (_completed) return;
            Debug.Log($"<color=#5d14c4>pase el if de complete</color>");

            _completed = true;
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

        protected override void Fireflies()
        {
            if (!_active) return;
            if (_fliesActive) return;
            if (GameManager.Instance.Player.actualRoom != GetComponent<RoomTrigger>().roomIndex) return;

            if (_completed)
            {
                PointToOtherRooms();
                return;
            }

            _fliesActive = true;

            Fireflies newFlies;

            newFlies = Instantiate(_fireflies, _discoBall.transform.position, Quaternion.identity);
            newFlies.SetFocusObj(_discoBall.transform);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }
}
