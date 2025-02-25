using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    [RequireComponent(typeof(BoxCollider))]
    public class CanillaLavadero : Obj_Interactuable, IInteractable
    {
        bool _active;
        public bool active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }

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

        public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error, int playerLevel)
        {
            Debug.Log($"<color=red>JAJA miralo, agarrando el aire</color>");
        }

        public void Interact()
        {
            if (_active)
            {
                _active = false;
                Debug.Log($"<color=red> Canilla  </color>");
            }
            else
            {
                _active = true;
                Debug.Log($"<color=green> Canilla prendida </color>");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (active && other.transform.TryGetComponent<IWhaterContainer>(out var container) && !container.CheckWather())
                container.GetWather();
        }
    }
}
