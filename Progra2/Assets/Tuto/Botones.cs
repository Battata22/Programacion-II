using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [SerializeField] GameObject spawn, abuela;
    [SerializeField] bool tornado = false, moco = false, sombra = false;
    [SerializeField] CreatePlayerTrap createScript;
    [SerializeField] TornadoItems tornadoScript;
    [SerializeField] CreateShadow shadowScript;
    [SerializeField] MocoSpawn mocoScript;

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
            Instantiate(abuela, spawn.transform.position, Quaternion.identity);
        }
        else if (sombra)
        {
            print(gameObject.name);
            SelectorUI.habAct = 2;
            SelectorUI.habilitiesManager += shadowScript.SpawnShadow;
            createScript.CreateTrap(spawn.transform.position);
            Instantiate(abuela, spawn.transform.position, Quaternion.identity);
        }
        else if (moco)
        {
            print(gameObject.name);
            SelectorUI.habAct = 4;
            SelectorUI.habilitiesManager += mocoScript.SetMoco;
            createScript.CreateTrap(spawn.transform.position);
            Instantiate(abuela, spawn.transform.position, Quaternion.identity);  
        }

        SelectorUI.habilitiesManager -= tornadoScript.Tornado;
        SelectorUI.habilitiesManager -= shadowScript.SpawnShadow;
        SelectorUI.habilitiesManager -= mocoScript.SetMoco;

    }
}
