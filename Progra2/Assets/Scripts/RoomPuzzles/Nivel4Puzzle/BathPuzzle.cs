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
        [SerializeField] public RoomTrigger myRoom;
        [SerializeField] HouseExitTrigger _houseExit;
        [SerializeField] InodoroPuzzle _inodoroPuzzle;
        [SerializeField] GameObject[] _charcos;
        [SerializeField] ElectronicoPuzzle _electronicoInRoom;

        //a
        [SerializeField] PapelPuzzle[] _papeles;

        bool _pisoMojado = false;
        bool _puzzleCompleted = false;

        //bool _active=false;

        private void Awake()
        {
            myRoom = GetComponent<RoomTrigger>();
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CompletePuzzle();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_active && other.gameObject.GetComponent<Player>())
                ChangeText();

            //if (other.TryGetComponent<SFX>(out var electronico))
            //{
            //    var poronga = electronico.gameObject.AddComponent<ElectronicoPuzzle>();
            //    poronga.Initialize(this, myRoom);
            //    _electronicoInRoom.Add(poronga);
            //}

            if (_puzzleCompleted && other.gameObject == _houseOwner.gameObject)
            {
                //Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");

                _houseOwner.StartKickOut(3f);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            //if(other.TryGetComponent<ElectronicoPuzzle>(out var pingo))
            //{
            //    _electronicoInRoom.Remove(pingo);
            //    Destroy(pingo);
            //}

            if (GameManager.Instance.Player.actualRoom != GetComponent<RoomTrigger>().roomIndex && GameManager.Instance._objectiveText.text == _objectiveTexts[_textIndex])
            {
                GameManager.Instance.ChangeObjectiveText("Busca a alguien para asustar");
            }
        }

        void CompletePuzzle()
        {
            if(_puzzleCompleted) return;
            _puzzleCompleted = true;

            _houseOwner.AddToCompleteList(this);

            _textIndex = _objectiveTexts.Length - 1;
            ChangeText();
        }

        public override void ActivatePuzzle()
        {
            //Debug.Log($"<color=red> Activar Puzzle no hace nada XDD </color>");

            _active = true;
        }

        public void ActivarAgua()
        {
            if(_pisoMojado) return;
            _pisoMojado = true;

            Debug.Log("<color=#9999FF>Puzzle Activo las aguas puercas </color>");
            foreach (var charco in _charcos)
            {
                charco.gameObject.SetActive(true);
            }

            _textIndex = 2;
            ChangeText();
        }

        public override void KickOutNpc()
        {
            foreach (var npc in _myNpc)
            {
                npc.OnRagdollEnd -= CompletePuzzle;
                _houseExit.AddToList(npc.gameObject);
                npc.InfiniteScared(true);
                npc.GetScared(1f, -1, _houseExit.transform);
            }

            doConstantScare = true;
            StartCoroutine(ConstantScare());
        }

        public void ZapitiZapZap()
        {
            if(!_pisoMojado) return;

            Debug.Log("<color=50a0ff>Chispitas de amor</color>");

            foreach(var npc in _myNpc)
            {
                npc.OnRagdollEnd += CompletePuzzle;
                npc.CallRagdollOn(npc.transform.up);
                npc.CallRagdollOff(1f, true);
            }
        }

        bool doConstantScare = false;
        IEnumerator ConstantScare()
        {
            while (doConstantScare)
            {
                foreach (var npc in _myNpc)
                {
                    if (npc.gameObject.activeInHierarchy)
                        npc.GetScared(1f, -1, _houseExit.transform);
                    doConstantScare = npc.gameObject.activeInHierarchy;
                }

                Debug.Log($"<color=red>Puzzle asustando constantemente</color>");

                yield return new WaitForSeconds(1f);
            }
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

            switch (_textIndex)
            {
                case 0:
                    var index = GetRandomPaper();
                    DoShit(_papeles[index].transform);
                    break;
                case 1:
                    DoShit(_inodoroPuzzle.transform);
                    break;
                case 2:
                    DoShit(_electronicoInRoom.transform);
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
            newFlies.SetFocusObj(newTarget);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }

        int GetRandomPaper()
        {
            int num = Random.Range(0,_papeles.Length);

            while (_papeles[num].used)
            {
                num = Random.Range(0, _papeles.Length);
            }

            return num;
        }

        public void InodoroTapado()
        {
            _textIndex = 1;
            ChangeText();
        }
    }
}
