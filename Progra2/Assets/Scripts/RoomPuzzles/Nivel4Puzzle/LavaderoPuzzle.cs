using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    public class LavaderoPuzzle : MonoBehaviour
    {
        //Hice esto para seguir los pasos del lavadero
        //pero el funcionamiento del lavadero no depende de este "puzzle"

        [SerializeField] CanillaLavadero _canilla;
        [SerializeField] Bucket _balde;
        [SerializeField] ParlanteGameplayStart _parlanteGameplayStart;

        [SerializeField] Fireflies _fliesPrefab;
        [SerializeField] GameObject[] _flechas;

        [SerializeField] KeyCode _fliesKey = KeyCode.V;

        bool _fliesActive = false;

        [SerializeField] protected string[] _objectiveTexts;
        protected int _textIndex = 0;

        private void Update()
        {
            if(!_fliesActive && Input.GetKeyDown(_fliesKey)) 
            {
                ActivateFlies();
            }
        }      

        public void ActivarPuzzle()
        {
            _textIndex = 0;
            ChangeText();

            _flechas[0].SetActive(true);

            _canilla.OnCanillaOn += TextoCanilla;
            _balde.OnGetWather += TextoWatha;
        }

        void TextoCanilla()
        {
            _textIndex = 1;
            ChangeText();

            _flechas[1].SetActive(true);
        }

        void TextoWatha()
        {
            _textIndex = 2;
            ChangeText();

            _flechas[0].SetActive(false);
            _flechas[2].SetActive(true);
        }

        public void ChauFlechitas()
        {
            foreach(var flecha in _flechas)
            {
                flecha.SetActive(false);
            }
        }


        void ActivateFlies()
        {
            //si parlante desactivado no hagas nada a la mierda, inutil de quinta
            if(!_parlanteGameplayStart.isActiveAndEnabled)
            {
                return;
            }
            
            //no esta prendido y no tiene agua, apuntar a canilla
            if (!_canilla.active && !_balde.CheckWather())
            {
                CreateFlies(_canilla.transform);
                CreateFlies(_balde.transform);
            }
            
            //activo y sin agua, apuntar a balde
            if(_canilla.active && !_balde.CheckWather())
            {
                CreateFlies(_balde.transform);
            }
            
            //tiene agua y parlante activo, apuntar al parlante y balde
            if(_balde.CheckWather() && _parlanteGameplayStart.isActiveAndEnabled)
            {
                CreateFlies(_balde.transform);
                CreateFlies(_parlanteGameplayStart.transform);
            }
        }

        void CreateFlies(Transform newTarget)
        {
            _fliesActive = true;

            Fireflies newFlies;


            newFlies = Instantiate(_fliesPrefab, newTarget.position, Quaternion.identity);
            newFlies.SetFocusObj(newTarget);

            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }

        protected void DeactivateFlies()
        {
            _fliesActive = false;
        }

        protected void ChangeText()
        {
            GameManager.Instance.ChangeObjectiveText(_objectiveTexts[_textIndex]);
        }
    }
}
