using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaCatolicaPuzzle
{
    public class NpcCocinaPuzzle : MonoBehaviour
    {
        Asustable _myAsustable;
        public bool carringMilk = false;
        PuzzleLvl3Cocina _myPuzzle;

        public bool taQuePela = false;
        public bool tieneChocolate = false;

        [SerializeField] Chocotorta _chotocorta;
        [SerializeField] Transform _npcHando;

        Chocotorta _newChoco;

        //a
        public void Initialize(PuzzleLvl3Cocina newPuzzle, Chocotorta newChoto, Transform Tha_Hando)
        {
            Debug.Log("<color=green>asdasdasd iniciado</color>");
            _myPuzzle = newPuzzle;

            _chotocorta = newChoto;
            _npcHando = Tha_Hando;
        }

        private void Awake()
        {
            Debug.Log($"<color=green>{this.name} existe</color>");
            _myAsustable = GetComponent<Asustable>();
        }

        public void StartMicrowave()
        {
            //go to pos
            //prender shit
            //start count down
            Debug.Log($"<color=#aaf90f> Npc start microwave</color>");

            _myAsustable.SetSpecificDestination(_myPuzzle.microwave.transform);
            _myPuzzle.microPuzzle.startCheck = true;
            //_myAsustable.OnNodePosition += TurnOnMicrowave;


        }

        //void TurnOnMicrowave()
        //{
        //    _myAsustable.OnNodePosition -= TurnOnMicrowave;

        //}

        public void GoTakeDessert()
        {
            Debug.Log($"<color=#aaf90f> Npc take dessert </color>");

            _myAsustable.SetSpecificDestination(_myPuzzle._fridge.transform);
            _myPuzzle._fridge.PrenderHeladera();
            _myAsustable.OnScareEnd -= GoTakeDessert;

            _myPuzzle._fridge.DaleElChocolate += GetChocolateCasero;


        }

        public void GetChocolateCasero()//codeo bien void, no apto para artistas
        {

            if (tieneChocolate) return;
            tieneChocolate = true;
            _myPuzzle._fridge.DaleElChocolate -= GetChocolateCasero;

            Debug.Log($"<color=#aaf90f>Debug para demostrar que el event no esta vacio y no soy un boludo (que capo que soy)</color>");

            _newChoco = Instantiate(_chotocorta, _npcHando.position, Quaternion.identity);
            //_newChoco.transform.localPosition = Vector3.zero;
            _newChoco.Initialize(_npcHando, _myAsustable, _myPuzzle);

            _myAsustable.OnSlideStop += _myPuzzle.CompletePuzzle;
            _myAsustable.OnRagdollEnd += _myPuzzle.CompletePuzzle;
        }

        public void GoCheckMicrowave()
        {
            Debug.Log($"<color=#aaf90f> Npc Check microwave</color>");

            _myAsustable.SetSpecificDestination(_myPuzzle.microwave.transform);
            _myPuzzle.microPuzzle.doCheck = true;
        }

        public void DrestroyMeBaby()
        {
            Destroy(this);
        }

        public void QuemarNpc()
        {
            Debug.Log($"<color=#aaf90f> Npc quemado</color>");

            _myPuzzle.ChangeState();

            _myAsustable.GetScared(1, -1);

            _myAsustable.OnScareEnd += GoTakeDessert;
        }

        private void OnDestroy()
        {
            //borrar todo los eventos
        }
    }
}