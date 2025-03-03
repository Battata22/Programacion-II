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
        [SerializeField] protected float _checkDist;
        [SerializeField] protected CuartoColeccionPuzzzle _myPuzzle;
        protected bool _broken = false;
        public bool broken { get { return _broken; } }

        protected DelegateType.VoidDelegate DoCheck = delegate { };

        [SerializeField] protected GameObject _myFlecha;

        protected void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Objeto");
        }

        protected void Update()
        {
            DoCheck();
        }

        public virtual void Interact()
        {
            Debug.Log($"<color=yellow> Veni {_myPuzzle._npcInRoom.name} culo roto </color>");

            _myPuzzle._npcInRoom.SetSpecificDestination(transform);

            DoCheck = DetectNpc;
            active= true;
        }

        protected void DetectNpc()
        {
            if (!active) return;

            var fakePos = new Vector3(transform.position.x, _myPuzzle._npcInRoom.transform.position.y, transform.position.z);
            if(!_broken && Vector3.SqrMagnitude(_myPuzzle._npcInRoom.transform.position - fakePos) < (_checkDist * _checkDist))
            {
                BreakObj();
            }
        }

        protected virtual void BreakObj()
        {
            if (_broken) return;
            Debug.Log($"<color=yellow>Objeto {name} perdio un valo de ${Random.Range(20,10001)} </color>");
            _broken = true;

            _myPuzzle.ObjectBroken();

            if(_myFlecha != null)
                _myFlecha.SetActive(false);

        }

    }
}