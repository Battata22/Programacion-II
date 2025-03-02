using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CasaFiesta
{
    public class PlayerUltimateFiesta : MonoBehaviour
    {
        [SerializeField] bool _ultimateActive = false;
        [SerializeField] ParticleSystem _sparky;

        [SerializeField] HoseOwner _nig;

        public bool ultimateActive { get { return _ultimateActive; } }

        private void Update()
        {
            if(!_ultimateActive && GameManager.Instance.terrorBar.isActiveAndEnabled && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
            {
                ActivateUltimate();
            }

            if(!ultimateActive && Input.GetKeyDown(KeyCode.P)) 
            {
                ActivateUltimate();
            }
        }

        void ActivateUltimate()
        {
            if (_ultimateActive) return;
            _ultimateActive = true;

            Debug.Log("<color=green>ULTI ACTIVA tenes 10 segundos</color>");

            //StartCoroutine(Ganar());
        }

        public void DestroyElectronics(List<Transform> electronicsInRoom)
        {
            if (!_ultimateActive) return;

            StartCoroutine(DoDestroys(electronicsInRoom));
            Callejeros();
        }

        IEnumerator DoDestroys(List<Transform> electronicsInRoom)
        {
            var ga = _nig.GetComponent<Asustable>();

            foreach (var electronic in electronicsInRoom)
            {
                Debug.Log($"<color=yellow> PUUUM {electronic.name} </color>");

                if (electronic.TryGetComponent<Rigidbody>(out var rb))
                {
                    var dir = electronic.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    
                    rb.constraints = RigidbodyConstraints.None;
                    rb.useGravity = true;

                    rb.AddForce(dir.normalized * 4f, ForceMode.VelocityChange);
                }

                if(electronic.TryGetComponent<Luces>(out var light))
                {
                    light.Quemar();
                }

                if(electronic.TryGetComponent<SFX>(out var sfx))
                {
                    sfx.Quemar();
                }

                var pikachu = Instantiate(_sparky,electronic.position,Quaternion.identity);
                Destroy(pikachu, pikachu.main.duration + 3f);

                ga.GetScared(1, -1);
                yield return new WaitForSeconds(0.3f);
            }
        }

        int roomsDestroy = 0;
        bool winCalled = false;

        void Callejeros()
        {
            roomsDestroy++;
            if (roomsDestroy >= 7 && !winCalled)
            {
                winCalled = true;
                StartCoroutine(Ganar());
            }
        }

        IEnumerator Ganar()
        {
            //yield return new WaitForSeconds(10f);
            
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