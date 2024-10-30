using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatePlayerTrap : MonoBehaviour
{
    //public delegate void VoidDelegate();
    //public VoidDelegate currentAbility;

    public DelegateType.VoidDelegate currentAbility;

    [SerializeField] PlayerTrap trapPrefab;
    public KeyCode trapKey = KeyCode.F;
    [SerializeField] float _trapCD, counterNum;
    [SerializeField] Slider trapSliderGenerica;
    static Slider trapSlider;
    float counterTimer;


    private void Start()
    {
        trapSlider = trapSliderGenerica;
    }

    private void Update()
    {
        if (SelectorUI.habAct != 5 && SelectorUI.habAct != 3)
        {
            if (Input.GetKeyDown(trapKey))
            {
                CreateTrap();
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
        //}


        trapSliderGenerica.value = trapSlider.value;
    }

    public void CreateTrap()
    {
        currentAbility = SelectorUI.habilitiesManager;
        //crear Trampa
        var newTrap = Instantiate(trapPrefab, transform.position, Quaternion.identity);
        newTrap.Initialize(this, currentAbility, _trapCD);
        //Iniciar trampa
    }


    public static void TrapActivada()
    {

        if (trapSlider.value < 0)
        {
            trapSlider.value++;
            GameManager.Instance.Master1.ActivarGB();
        }
        else
        {
            trapSlider.value++;
        }

        if (trapSlider.value >= trapSlider.maxValue)
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    void Test()
    {
        print($"<color=#C8318F> Funcion  del Create Trap </color>");
    }
}
