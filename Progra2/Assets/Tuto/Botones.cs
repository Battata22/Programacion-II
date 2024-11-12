using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    [SerializeField] bool tornado = false, moco = false, sombra = false;
    [SerializeField] CreatePlayerTrap createScript;
    [SerializeField] TornadoItems tornadoScript;

    void Start()
    {
        createScript = GameManager.Instance.Player.GetComponent<CreatePlayerTrap>();
    }


    void Update()
    {
    }

    public void Trap()
    {
        if (tornado)
        {
            print(gameObject.name);
            SelectorUI.habAct = 1;
            SelectorUI.habilitiesManager += tornadoScript.Tornado;
            createScript.CreateTrap(spawn.transform.position);
        }
        else if (sombra)
        {
            print(gameObject.name);
            SelectorUI.habAct = 2;
            createScript.CreateTrap(spawn.transform.position);
        }
        else if (moco)
        {
            print(gameObject.name);
            SelectorUI.habAct = 4;
            createScript.CreateTrap(spawn.transform.position);
        }

        SelectorUI.habilitiesManager -= tornadoScript.Tornado;

    }
}
