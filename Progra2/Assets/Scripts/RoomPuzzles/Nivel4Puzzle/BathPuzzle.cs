using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    public class BathPuzzle : Nivel4Puzzle
    {
        /*Idea
         * tapar el inodoro con papeles
         * llenar el piso de agua
         * tirar un electronico prendido al piso
         * tomscream.sfx
        */

        [SerializeField] Asustable[] _myNpc;
        //[SerializeField] Transform _exit;
        [SerializeField] RoomTrigger _myRoom;
        [SerializeField] HouseExitTrigger _houseExit;
        [SerializeField] InodoroPuzzle _inodoroPuzzle;
        [SerializeField] GameObject[] _charcos;
        [SerializeField] List<ElectronicoPuzzle> _electronicosInRoom = new();

        private void Awake()
        {
            _myRoom = GetComponent<RoomTrigger>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<SFX>(out var electronico))
            {
                var poronga = electronico.gameObject.AddComponent<ElectronicoPuzzle>();
                _electronicosInRoom.Add(poronga);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<ElectronicoPuzzle>(out var pingo))
            {
                _electronicosInRoom.Remove(pingo);
                Destroy(pingo);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CompletePuzzle();
            }
        }

        void CompletePuzzle()
        {
            foreach (var npc in _myNpc)
            {
                _houseExit.AddToList(npc.gameObject);
                npc.InfiniteScared(true);
                npc.GetScared(1f, -1, _houseExit.transform);
            }
        }

        public override void ActivatePuzzle()
        {
            Debug.Log($"<color=red> Activar Puzzle no hace nada XDD </color>");
        }

        public void ActivarAgua()
        {
            Debug.Log("<color=#9999FF>Puzzle Activo las aguas puercas </color>");
        }

        public override void KickOutNpc()
        {
            throw new System.NotImplementedException();
        }
    }
}
