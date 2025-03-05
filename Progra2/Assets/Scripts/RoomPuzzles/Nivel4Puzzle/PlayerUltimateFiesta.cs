using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CasaFiesta
{
    public class PlayerUltimateFiesta : MonoBehaviour
    {
        [SerializeField] bool _ultimateActive = false;
        [SerializeField] ParticleSystem _sparky;

        [SerializeField] HoseOwner _ga;
        [SerializeField] string[] _ultiText;

        [SerializeField] Asustable _houseOwner;
        [SerializeField] AudioSource _myAudio;
        [SerializeField] AudioClip[] _explotido;

        [SerializeField] Light[] luces;

        public static bool ultimateActive { get; protected set; }

        private void Update()
        {
            if (!_ultimateActive && GameManager.Instance.terrorBar.isActiveAndEnabled && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
            {
                ActivateUltimate();
            }

            if (!ultimateActive && Input.GetKeyDown(KeyCode.P))
            {
                ActivateUltimate();
            }
        }

        void ActivateUltimate()
        {
            if (_ultimateActive) return;
            _ultimateActive = true;
            ultimateActive = true;

            Debug.Log("<color=green>ULTI ACTIVA tenes 10 segundos</color>");

            ChangeText($"Destruye las habitaciones \n Habitaciones rotas {roomsDestroy} de 7");

            _houseOwner.StopUseOwnNode();


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
            var nig = _ga.GetComponent<Asustable>();

            foreach (Light luz in luces)
            {
                luz.enabled = false;
            }

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

                if (electronic.TryGetComponent<Luces>(out var light))
                {
                    light.Quemar();
                }


                if (electronic.TryGetComponent<SFX>(out var sfx))
                {
                    sfx.Quemar();
                }

                _myAudio.PlayOneShot(_explotido[Random.Range(0,_explotido.Length)]);

                var pikachu = Instantiate(_sparky, electronic.position, Quaternion.identity);
                Destroy(pikachu, pikachu.main.duration + 3f);

                nig.GetScared(1, -1);
                yield return new WaitForSeconds(0.3f);
            }

        }

        int roomsDestroy = 0;
        bool winCalled = false;

        void Callejeros()
        {
            roomsDestroy++;

            ChangeText($"Destruye las habitaciones \n Habitaciones rotas {roomsDestroy} de 7");

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

                ChangeText($"YOU WIN");

                yield return new WaitForSeconds(0.3f);
            }

            GameManager.Instance.CompleteLevel();
        }

        protected void ChangeText(string text)
        {

            GameManager.Instance.ChangeObjectiveText(text);
            //GameManager.Instance.ChangeObjectiveText(_objectiveTexts[_textIndex]);
        }
    }
}