using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public abstract class Nivel4Puzzle : MonoBehaviour
    {
        [SerializeField] protected HoseOwner _houseOwner;
        [SerializeField] protected Transform _posForHouseOwner;

        [SerializeField] protected string[] _objectiveTexts;
        protected int _textIndex = 0;

        [SerializeField] protected Transform _firefliesSpot;
        public Transform firefliesSpot { get { return _firefliesSpot; } }

        //fireflies shit
        [SerializeField] protected Fireflies _fireflies;
        [SerializeField] protected Nivel4Puzzle[] _nextPuzzles;
        [SerializeField] protected KeyCode _fliesActiveKey = KeyCode.V;

        [SerializeField] protected GameObject[] _flechas;

        protected bool _completed = false;
        protected bool _active = false;
        protected bool _fliesActive = false;

        public bool completed { get { return _completed; } }


        protected virtual void Update()
        {
            if (Input.GetKeyDown(_fliesActiveKey))
            {
                Fireflies();
            }
        }

        abstract public void ActivatePuzzle();
        public virtual Transform GiveDestination()
        {
            return _posForHouseOwner;
        }

        abstract public void KickOutNpc();

        protected void ChangeText()
        {
            if (PlayerUltimateFiesta.ultimateActive) return;
            GameManager.Instance.ChangeObjectiveText(_objectiveTexts[_textIndex]);
        }

        protected void ChangeText(string text)
        {
            GameManager.Instance.ChangeObjectiveText(text);

        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if(GameManager.Instance.Player.actualRoom != GetComponent<RoomTrigger>().roomIndex && GameManager.Instance._objectiveText.text == _objectiveTexts[_textIndex])
            {
                GameManager.Instance.ChangeObjectiveText("Busca a alguien para asustar");
            }
        }

        protected abstract void Fireflies();

        protected void DeactivateFlies()
        {
            _fliesActive = false;
        }

        protected void PointToOtherRooms()
        {
            //oh fuck
            //oh god
            //what have i done?
            //i hate myself and my lazy ass
            //lets see how i can fix this;

            var index = GetRandomRoom();

            _fliesActive = true;

            Fireflies newFlies;

            newFlies = Instantiate(_fireflies, _nextPuzzles[index].transform.position, Quaternion.identity);
            newFlies.SetFocusObj(_nextPuzzles[index].firefliesSpot);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }

        protected int GetRandomRoom()
        {
            int newRoom = Random.Range(0, _nextPuzzles.Length);

            while (_nextPuzzles[newRoom].completed)
            {
                newRoom = Random.Range(0, _nextPuzzles.Length);
            }

            return newRoom;
        }
    }
}
