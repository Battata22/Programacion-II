using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static event DelegateType.VoidDelegate TrapPhaseActive;
    public static event DelegateType.VoidDelegate GameplayPhaseActive;

    bool trapPhase = true;

    private void Awake()
    {
        TrapPhaseActive = delegate { };
        GameplayPhaseActive = delegate { };
    }

    private void Start()
    {
        ViejaDurmiente.startRun += GameplayPhase;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            print("F8 presionado");
            if(trapPhase)
            {
                GameplayPhase();
            }
            else
            {
                TrapPhase();
            }
        }
    }

    void TrapPhase()
    {
        //Desactivar NPCS
        //Trampas no interactuables
        //Trampas repocicionables?
        //Trampas spawneables
        TrapPhaseActive();
        trapPhase = true;
    }

    void GameplayPhase()
    {
        //Activar NPCs (asustables y mascotas)
        //Trampas Interactuables
        //Trampas no repocicionables?
        //Trampas no spawneables
        GameplayPhaseActive();
        trapPhase= false;
    }
}
