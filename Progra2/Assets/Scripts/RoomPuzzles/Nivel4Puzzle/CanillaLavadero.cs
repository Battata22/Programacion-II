using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaFiesta
{
    [RequireComponent(typeof(BoxCollider))]
    public class CanillaLavadero : MonoBehaviour, IInteractable
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
