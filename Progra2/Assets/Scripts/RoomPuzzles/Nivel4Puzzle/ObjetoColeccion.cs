using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    [RequireComponent(typeof(BoxCollider))]
    public class ObjetoColeccion : Obj_Interactuable, IInteractable
    {
        [SerializeField] public bool active;
        [SerializeField] protected float _checkDist;
        [SerializeField] protected CuartoColeccionPuzzzle _myPuzzle;
        protected bool _broken = false;
        public bool broken { get { return _broken; } }

        protected DelegateType.VoidDelegate DoCheck = delegate { };

        [SerializeField] protected GameObject _myFlecha;

        [SerializeField] GameObject _myPreciso;

        protected void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Objeto");
        }

        private void Start()
        {
            _renderer = GetComponent<Renderer>();

            if (_renderer != null)
            {
                foreach (var mat in _renderer.materials)
                {
                    //Debug.Log($"<color=yellow> La recalcada concha de su madre {mat.name}</color>");

                    //print(mat.name);
                    if (mat.name == "M_Outline (Instance)")
                    {
                        OutLine = mat;
                        _OGthik = OutLine.GetFloat("_Thickness");
                        OutLine.SetFloat("_Thickness", 0f);
                        OutLine.SetFloat("_Active", 0);
                    }
                }
            }
        }

        protected void Update()
        {
            DoCheck();
        }

        public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error, int playerLevel)
        {
            //if (_onCd) return;
            //Debug.Log("<color=#0bf1ff> Interact.wav </color>");

            //StartCoroutine(Coso());

            //MyInteract();

            Debug.Log("<color=red> PKJKJNAASPKJ </color>");

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

            if(_myPreciso != null && _myPreciso.TryGetComponent<Rigidbody>(out var pp))
            {
                pp.constraints = RigidbodyConstraints.None;

                pp.AddForce(pp.transform.right * 6f, ForceMode.VelocityChange);

                _myPreciso.GetComponent<Pickable>().ignorePickUp = false;
            }

        }

    }
}