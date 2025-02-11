using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public class ElectronicoPuzzle : MonoBehaviour
    {
        [SerializeField] SFX _myElectronic;

        [SerializeField] bool _active;
        public bool active { get { return _active; }} 

        private void Awake()
        {
            _myElectronic = GetComponent<SFX>();
            _myElectronic.OnPlay += TamoActivoPapi;
            _myElectronic.OnStop += LaMeme;
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
