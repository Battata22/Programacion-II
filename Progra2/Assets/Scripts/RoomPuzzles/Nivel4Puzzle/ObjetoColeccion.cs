using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    [RequireComponent(typeof(BoxCollider))]
    public class ObjetoColeccion : MonoBehaviour, IInteractable
    {
        [SerializeField] public bool active;
        [SerializeField] float _checkDist;
        [SerializeField] CuartoColeccionPuzzzle _myPuzzle;
        bool _broken = false;
        public bool broken { get { return _broken; } }

        DelegateType.VoidDelegate DoCheck = delegate { };

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Objeto");
        }

        private void Update()
        {
            DoCheck();
        }

        public void Interact()
        {
            Debug.Log($"<color=yellow> Veni {_myPuzzle._npcInRoom.name} culo roto </color>");

            _myPuzzle._npcInRoom.SetSpecificDestination(transform);

            DoCheck = DetectNpc;
            active= true;
        }

        void DetectNpc()
        {
            if (!active) return;

            var fakePos = new Vector3(transform.position.x, _myPuzzle._npcInRoom.transform.position.y, transform.position.z);
            if(!_broken && Vector3.SqrMagnitude(_myPuzzle._npcInRoom.transform.position - fakePos) < (_checkDist * _checkDist))
            {
                BreakObj();
            }
        }

        void BreakObj()
        {
            if (_broken) return;
            Debug.Log($"<color=yellow>Objeto {name} perdio un valo de ${Random.Range(20,10001)} </color>");
            _broken = true;

            _myPuzzle.ObjectBroken();
        }

    }
}