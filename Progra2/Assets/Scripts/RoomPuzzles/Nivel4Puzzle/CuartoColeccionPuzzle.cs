using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    public class CuartoColeccionPuzzzle : Nivel4Puzzle
    {
        /*
         * idea
         * Cuarto lleno de mierdas coleccionables
         * ir activando las weas
         * despues de romper un par de cosas el dueño de la casa aparece
         * el dueño se enoja y hecha a la mierda al borrachin
        */

        [SerializeField] public Asustable _npcInRoom;
        [SerializeField] ObjetoColeccion[] _coleccionables;
        [SerializeField, Tooltip("X objects broke to complete puzzle")] int hasToBreak;
        int _brokenObjs = 0;
        [SerializeField] HouseExitTrigger _houseExit;

        //bool _completed = false;
        bool llamandoDuenho = false;

        //bool _active = false;

        protected override void Update()
        {
            //    if (Input.GetKeyDown(KeyCode.UpArrow))
            //    {
            //        CompletePuzzle();
            //    }
            base.Update();
        }

        public void CompletePuzzle()
        {
            Debug.Log($"<color=#5d14c4>entre a complete</color>");

            if (_completed) return;
            Debug.Log($"<color=#5d14c4>pase el if de complete</color>");

            //llamado cuando se rompen X coleccionables
            //llamar duenho
            //hacer que borracho salga de casa

            _completed = true;
            _houseOwner.AddToCompleteList(this);

            _textIndex = 1;
            ChangeText();

            //completed = true;

            //llamandoDuenho=true;
            //StartCoroutine(LlamarOwner());
            ////_houseOwner.SetSpecificDestination(_npcInRoom.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_active && other.gameObject.GetComponent<Player>())
            {
                if (_textIndex == 0)
                    ChangeText($"Los Juguetes son para jugar \n Objetos rotos {_brokenObjs} de {hasToBreak}");
                else
                    ChangeText();
            }

            if (_completed && other.gameObject == _houseOwner.gameObject)
            {
                //llamandoDuenho = false;
                //Invoke("KickOut", 3f);

                Debug.Log($"<color=red>{name} detecte al HouseOwner {_houseOwner.name}</color>");
                _houseOwner.StartKickOut(3f);
            }
        }

        #region comment
        //IEnumerator LlamarOwner()
        //{
        //    var wait = new WaitForSeconds(0.3f);
        //    while (llamandoDuenho)
        //    {
        //        _houseOwner.SetSpecificDestination(_npcInRoom.transform);

        //        yield return wait;
        //    }
        //}

        //void KickOut()
        //{
        //    Debug.Log($"<color=#5d14c4>echando a borracho</color>");

        //    _houseExit.AddToList(_npcInRoom.transform.gameObject);
        //    _npcInRoom.InfiniteScared(true);
        //    _npcInRoom.GetScared(1,-1, _houseExit.transform);
        //} 
        #endregion

        public override void ActivatePuzzle()
        {
            Debug.Log("Huh?");

            _active = true;
        }

        public void ObjectBroken()
        {
            Debug.Log($"<color=#5d14c4>Entre a romper</color>");
            _brokenObjs++;


            _textIndex = 0;
            if (!_completed)
                ChangeText($"Los Juguetes son para jugar \n Objetos rotos {_brokenObjs} de {hasToBreak}");

            if (_brokenObjs >= hasToBreak)
                CompletePuzzle();
        }

        public override void KickOutNpc()
        {
            Debug.Log($"<color=#5d14c4>echando a borracho</color>");


            _houseExit.AddToList(_npcInRoom.gameObject);
            _npcInRoom.InfiniteScared(true);
            _npcInRoom.GetScared(1, -1, _houseExit.transform);

            StartCoroutine(ConstantScare());
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

            var index = GetRandomObject();

            newFlies = Instantiate(_fireflies, _coleccionables[index].transform.position, Quaternion.identity);
            newFlies.SetFocusObj(_coleccionables[index].transform);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }

        int GetRandomObject()
        {
            int num = Random.Range(0, _coleccionables.Length);

            while (_coleccionables[num].broken)
                num = Random.Range(0, _coleccionables.Length);

            return num;
        }

        IEnumerator ConstantScare()
        {
            while (_completed)
            {
                

                if (_npcInRoom.gameObject.activeInHierarchy)
                    _npcInRoom.GetScared(1f, -1, _houseExit.transform);
                _completed = _npcInRoom.gameObject.activeInHierarchy;

                Debug.Log($"<color=red>Puzzle asustando constantemente</color>");

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
