using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CasaFiesta
{
    public class ElectronicsInRoom : MonoBehaviour
    {
        //a
        [SerializeField] List<Transform> _myElectronicos;
        bool _esplotido = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent<PlayerUltimateFiesta>(out var ultimate) && ultimate.ultimateActive)
            {
                ExplodeElectronis(ultimate);
            }
        }

        void ExplodeElectronis(PlayerUltimateFiesta ultimate)
        {
            if (_esplotido) return;
            _esplotido = true;

            ultimate.DestroyElectronics(_myElectronicos);
        }
    }
}