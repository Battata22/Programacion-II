using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerTrap : MonoBehaviour
{
    //public delegate void VoidDelegate();
    //public VoidDelegate currentAbility;

    public DelegateType.VoidDelegate currentAbility;

    [SerializeField] PlayerTrap trapPrefab;

    private void Awake()
    {
        //currentAbility = SelectorUI.habilitiesManager;
        //print(currentAbility);
    }

    private void Update()
    {
        if (SelectorUI.habAct != 5 && SelectorUI.habAct != 3)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CreateTrap();
            }
        }

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    CreateTrap();
        //}

    }

    public void CreateTrap()
    {
        currentAbility = SelectorUI.habilitiesManager;
        //crear Trampa
        var newTrap = Instantiate(trapPrefab, transform.position, Quaternion.identity);
        newTrap.Initialize(this, currentAbility);
        //Iniciar trampa
    }

    void Test()
    {
        print($"<color=#C8318F> Funcion  del Create Trap </color>");
    }
}
