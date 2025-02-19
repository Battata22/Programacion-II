using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CasaFiesta
{
    public class PlayerUltimateFiesta : MonoBehaviour
    {
        [SerializeField] bool _ultimateActive = false;
        [SerializeField] ParticleSystem _sparky;

        public bool ultimateActive { get { return _ultimateActive; } }

        private void Update()
        {
            if(!_ultimateActive && GameManager.Instance.terrorBar.isActiveAndEnabled && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
            {
                ActivateUltimate();
            }
        }

        void ActivateUltimate()
        {
            if (_ultimateActive) return;
            _ultimateActive = true;

            Debug.Log("<color=green>ULTI ACTIVA tenes 10 segundos</color>");

            StartCoroutine(Ganar());
        }

        public void DestroyElectronics(List<Transform> electronicsInRoom)
        {
            if (!_ultimateActive) return;

            StartCoroutine(DoDestroys(electronicsInRoom));
        }

        IEnumerator DoDestroys(List<Transform> electronicsInRoom)
        {
            foreach (var electronic in electronicsInRoom)
            {
                Debug.Log($"<color=yellow> PUUUM {electronic.name} </color>");

                if (electronic.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddForce(electronic.up, ForceMode.VelocityChange);
                }

                var pikachu = Instantiate(_sparky,electronic.position,Quaternion.identity);
                Destroy(pikachu, pikachu.main.duration + 3f);

                yield return new WaitForSeconds(0.3f);
            }
        }

        IEnumerator Ganar()
        {
            yield return new WaitForSeconds(10f);
            
            int aea = 0;

            while (aea < 5)
            {
                aea++;
                Debug.Log($"<color=red> GANASTE x{aea}</color>");

                yield return new WaitForSeconds(0.3f);
            }

            GameManager.Instance.CompleteLevel();
        }
    }
}