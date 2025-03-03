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
        [SerializeField] ParticleSystem _stinkGen;

        public RoomTrigger _otraHabitacion;

        //bool _completed = false;
        //bool _active = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_active && other.gameObject.GetComponent<Player>())
                ChangeText();
        }

        public void CompletePuzzle()
        {
            _completed = true;
            _houseOwner.AddToCompleteList(this);

            foreach(var door in _doors)
            {
                door.UnlockDoor();
            }

            _stinkGen.Play();

            _textIndex = _objectiveTexts.Length-1;
            ChangeText();

            foreach(var flecha in _flechas)
            {
                flecha.SetActive(false);
            }
        }

        public override void ActivatePuzzle()
        {
            //Debug.Log("<color=magenta> Bloquie las puertas porque me dan asco todos </color>");

            _active = true;

            foreach (var door in _doors)
            {
                door.LockDoor();
            }

            _cuadro.ignorePickUp = false;
            _armario.SetCanSpawn(true);

            _armario.OnObjectSpawn += BombaSpawneada;
            _cuadro.OnPickUp += SacarTapon;

            _flechas[0].SetActive(true);
        }

        public override void KickOutNpc()
        {
            foreach (var npc in _myNpc)
            {
                _houseExit.AddToList(npc.gameObject);
                npc.InfiniteScared(true);
                npc.GetScared(1f, -1, _houseExit.transform);
            }

            _completed = true;
            StartCoroutine(ConstantScare());
        }

        IEnumerator ConstantScare()
        {
            while (_completed)
            {
                foreach (var npc in _myNpc)
                {
                    if (npc.gameObject.activeInHierarchy)
                        npc.GetScared(1f, -1, _houseExit.transform);
                    _completed = npc.gameObject.activeInHierarchy;
                }

                Debug.Log($"<color=red>Puzzle asustando constantemente</color>");

                yield return new WaitForSeconds(1f);
            }
        }

        public bool CheckComplete(GameObject newObj)
        {
            //Debug.Log($"<color=green>Entre al check {_roomCompleted}</color>");
            if(_completed && newObj == _houseOwner.gameObject)
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

            _flechas[0].SetActive(false);
            _flechas[1].SetActive(true);

        }

        void BombaSpawneada()
        {
            _armario.OnObjectSpawn -= BombaSpawneada;

            _textIndex = 2;
            ChangeText();

            _flechas[1].SetActive(false);
            _flechas[2].SetActive(true);
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

        protected override void Fireflies()
        {
            if (!_active) return;
            if (_fliesActive) return;
            if (!(GameManager.Instance.Player.actualRoom == GetComponent<RoomTrigger>().roomIndex || GameManager.Instance.Player.actualRoom == _otraHabitacion.roomIndex)) return;

            if (base._completed)
            {
                PointToOtherRooms();
                return;
            }

            switch (_textIndex)
            {
                case 0:
                    DoShit(_tapon.transform);
                    break;
                case 1:
                case 2:
                    if (_armario.objectSpawned == null)
                        DoShit(_armario.transform);
                    else
                        DoShit(_armario.objectSpawned.transform);
                    break;
                default:
                    PointToOtherRooms();
                    break;

            }

            
        }

        void DoShit(Transform newTarget)
        {
            _fliesActive = true;

            Fireflies newFlies;

            newFlies = Instantiate(_fireflies, newTarget.position, Quaternion.identity);
            newFlies.SetFocusObj(newTarget.transform);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }
}