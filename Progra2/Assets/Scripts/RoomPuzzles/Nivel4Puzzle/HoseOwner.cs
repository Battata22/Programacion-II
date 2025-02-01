using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{

    public class HoseOwner : MonoBehaviour
    {
        Asustable _myAsus;

        [SerializeField] List<Nivel4Puzzle> _completedRooms = new();
        [SerializeField] float timeBtRooms;
        float _lastRoomCall = 0;
        public bool hasRoomsLeft = false;
        public bool goingToRoom = false;

        private void Awake()
        {
            _myAsus = GetComponent<Asustable>();
        }

        private void Update()
        {
            if (hasRoomsLeft && Time.time - _lastRoomCall > timeBtRooms)
            {
                GoToRoom();
                _lastRoomCall = Time.time;
            }
        }

        public void AddToCompleteList(Nivel4Puzzle newRoom)
        {
            bool addRoom = true;
            foreach(var room in _completedRooms)
            {
                if(room == newRoom)
                    addRoom = false;
            }

            if (addRoom)
            {
                _completedRooms.Add(newRoom);
                hasRoomsLeft = true;
            }
        }

        void GoToRoom()
        {
            goingToRoom = true;

            _myAsus.SetSpecificDestination(_completedRooms[0].GiveDestination());
        }

        public void StartKickOut(float wait)
        {
            StartCoroutine(KickOutNpc(wait));
        }

        IEnumerator KickOutNpc(float wait)
        {
            yield return new WaitForSeconds(wait);


            //gadget.Repair();
            //_brokenGadgets.Remove(gadget);

            //_isTringToRepair = false;

            //if (_brokenGadgets.Count < 1)
            //    _hasObjToRepair = false;

            //SetNewDestination(null);

            _completedRooms[0].KickOutNpc();
            _completedRooms.RemoveAt(0);

            goingToRoom=false;

            if(_completedRooms.Count <1)
                hasRoomsLeft=false;

            _myAsus.SetNewDestination(null);
        }
    }
}
