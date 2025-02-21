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
        [SerializeField] Pickable _cuadro;
        [SerializeField] TaponPared _tapon;
        [SerializeField] Door[] _doors;
        [SerializeField] HouseExitTrigger _houseExit;
        [SerializeField] ObjectSpawner _armario;

        public RoomTrigger _otraHabitacion;

        bool _roomCompleted = false;
        bool _active = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_active && other.gameObject.GetComponent<Player>())
                ChangeText();
        }

        public void CompletePuzzle()
        {
            _roomCompleted = true;
            _houseOwner.AddToCompleteList(this);

            foreach(var door in _doors)
            {
                door.UnlockDoor();
            }

            _textIndex = _objectiveTexts.Length-1;
            ChangeText();

        }

        public override void ActivatePuzzle()
        {
            Debug.Log("<color=magenta> Bloquie las puertas porque me dan asco todos </color>");

            _active = true;

            foreach (var door in _doors)
            {
                door.LockDoor();
            }

            _armario.OnObjectSpawn += BombaSpawneada;
            _cuadro.OnPickUp += SacarTapon;

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
            //Debug.Log($"<color=green>Entre al check {_roomCompleted}</color>");
            if(_roomCompleted && newObj == _houseOwner.gameObject)
            {
                Debug.Log("<color=green>SALTE DE AQUI PERRO</color>");

                return true;
            }
            //Debug.Log($"<color=green>Despues del if {_roomCompleted}</color>");

            return false;
        }

        void SacarTapon()
        {
            _cuadro.OnPickUp -= SacarTapon;

            //Sonido de Zelda
            _tapon.ChangeToTrigger();

            _textIndex = 1;
            ChangeText();

        }

        void BombaSpawneada()
        {
            _armario.OnObjectSpawn -= BombaSpawneada;

            _textIndex = 2;
            ChangeText();
        }

        public void SegundoTrigger(Collider other)
        {
            if (_active && other.gameObject.GetComponent<Player>())
                ChangeText();
        }

        protected override void OnTriggerExit(Collider other)
        {
            if ((GameManager.Instance.Player.actualRoom != GetComponent<RoomTrigger>().roomIndex && GameManager.Instance.Player.actualRoom != _otraHabitacion.roomIndex) && GameManager.Instance._objectiveText.text == _objectiveTexts[_textIndex])
            {
                GameManager.Instance.ChangeObjectiveText("Busca a alguien para asustar");
            }
        }
    }
}