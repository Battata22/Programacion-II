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
        [SerializeField] ParticleSystem _chispasGen;
        public bool active { get { return _active; }}

        [SerializeField] int puzzleIndex;

        private void Awake()
        {
            _myElectronic = GetComponent<SFX>();
            _myElectronic.OnPlay += TamoActivoPapi;
            _myElectronic.OnStop += LaMeme;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            puzzleIndex = _myPuzzle.transform.GetComponent<RoomTrigger>().roomIndex;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_active)
            {
                //Spawn Chispas
                var chispas = Instantiate(_chispasGen,transform.position, Quaternion.identity);
            }
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
