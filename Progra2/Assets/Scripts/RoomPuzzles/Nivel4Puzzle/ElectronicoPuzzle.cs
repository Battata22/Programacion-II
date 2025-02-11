using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public class ElectronicoPuzzle : MonoBehaviour
    {
        [SerializeField] SFX _myElectronic;

        [SerializeField] bool _active;
        [SerializeField] BathPuzzle _myPuzzle;
        public bool active { get { return _active; }}

        int puzzleIndex;

        private void Awake()
        {
            _myElectronic = GetComponent<SFX>();
            _myElectronic.OnPlay += TamoActivoPapi;
            _myElectronic.OnStop += LaMeme;
        }

        public void Initialize(BathPuzzle newPuzzle, RoomTrigger puzzleRoom)
        {
            puzzleIndex = puzzleRoom.roomIndex;
            _myPuzzle = newPuzzle;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(_active && collision.transform.GetComponent<Piso>() && _myElectronic.actualRoom == puzzleIndex)
            {
                _myPuzzle.ZapitiZapZap();
            }
        }

        void TamoActivoPapi()
        {
            _active = true;
        }

        void LaMeme()
        {
            _active=false;
        }
    }
}
