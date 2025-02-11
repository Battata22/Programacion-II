using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CasaFiesta
{
    public class InodoroPuzzle : MonoBehaviour,IInteractable
    {
        [SerializeField] int _maxHp;
        int _hp;
        [SerializeField] BathPuzzle _bathPuzzle;
        [SerializeField] float _useCd;

        bool _canGetDmg = true;
        bool _onCd = false;
        bool flooded = false;

        event DelegateType.VoidDelegate MyInteract = delegate { };

        void Awake() 
        {
            MyInteract = NormalUse;
            _hp = _maxHp;
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.transform.GetComponent<PapelPuzzle>())
        //        GetDamage();
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<PapelPuzzle>())
                GetDamage();
        }

        void GetDamage()
        {
            if (!_canGetDmg) return;
            StartCoroutine(DamageCD());

            Debug.Log("<color=red>Inodoro que habla</color>");

            _hp--;

            if(_hp <= 0 && !flooded)
            {
                _hp = 0;
                MyInteract = Flood;
            }
        }

        public void Interact()
        {
            if (_onCd) return;
            Debug.Log("<color=#0bf1ff> Interact.wav </color>");

            StartCoroutine(Coso());
            
            MyInteract();
        }

        void NormalUse()
        {
            //play flush noise
            Debug.Log("<color=#00ffff> Agua.wav </color>");
        }

        void Flood()
        {
            //a
            //sonido
            //+ activar agua
            //+ siguiente paso del puzzle

            Debug.Log("<color=#00ffff> MuchaAgua.wav </color>");


            _bathPuzzle.ActivarAgua();

            flooded = true;

            MyInteract = delegate { };
        }

        IEnumerator Coso()
        {
            _onCd = true;
            yield return new WaitForSeconds(_useCd);

            _onCd = false;
        }

        IEnumerator DamageCD()
        {
            _canGetDmg = false;
            yield return new WaitForSeconds(0.1f);

            _canGetDmg = true;
        }
    }
}
