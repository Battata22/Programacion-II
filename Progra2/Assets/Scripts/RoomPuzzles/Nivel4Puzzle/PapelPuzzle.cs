using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public class PapelPuzzle : MonoBehaviour
    {
        [SerializeField] GameObject _myArrow;

        //pingo
        bool _used = false;
        public bool used { get { return _used; } }

        public void UsePaper()
        {
            _used = true;
            if(_myArrow != null)
                _myArrow.SetActive(false);
        }

    }
}