using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatePlayerTrap : MonoBehaviour
{
    //public delegate void VoidDelegate();
    //public VoidDelegate currentAbility;

    public DelegateType.VoidDelegateTrans currentAbility;

    [SerializeField] PlayerTrap trapPrefab;
    public KeyCode trapKey = KeyCode.F;
    public float _trapCD, counterNum, maxTraps, currentTraps;
    [SerializeField] Slider trapSlider;
    [SerializeField] TextMeshProUGUI textoTraps;
    //Slider trapSlider;
    float counterTimer;



    //private void Start()
    //{
    //    trapSlider = trapSliderGenerica;
    //}

    private void Update()
    {
        textoTraps.text = ("Trampas Utilizadas: " + currentTraps + " de " + maxTraps);

        // 0 siempre, 6 7 y 8 por que todavia no existe trampa para ellos
        if (SelectorUI.habAct != 0 && SelectorUI.habAct != 3 && SelectorUI.habAct != 5 && SelectorUI.habAct != 6 && SelectorUI.habAct != 7 && SelectorUI.habAct != 8)
        {
            if (Input.GetKeyDown(trapKey) && currentTraps < maxTraps)
            {
                CreateTrap(transform.position);
            }
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            trapSlider.value++;
        }

        counterTimer += Time.deltaTime;

        //if (counterTimer >= counterNum)
        //{
        //    trapSlider.value--;
        //    counterTimer = 0;
        //}


        //trapSliderGenerica.value = trapSlider.value;
    }

    public void CreateTrap(Vector3 ubi)
    {
        currentTraps++;
        currentAbility = SelectorUI.habilitiesManager;
        //crear Trampa
        var newTrap = Instantiate(trapPrefab, ubi, Quaternion.identity);
        newTrap.OnTrapActive += TrapActivada;
        newTrap.Initialize(this, currentAbility, _trapCD);
        //Iniciar trampa
    }


    public void TrapActivada()
    {

        trapSlider.value++;

        if (trapSlider.value >= trapSlider.maxValue)
        {
            SceneManager.LoadScene("Victoria");
        }
        counterTimer = 0;
    }

    void Test()
    {
        print($"<color=#C8318F> Funcion  del Create Trap </color>");
    }
}
