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

        abstract public void ActivatePuzzle();
        public virtual Transform GiveDestination()
        {
            return _posForHouseOwner;
        }

        abstract public void KickOutNpc();

        protected void ChangeText()
        {
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
    }
}
