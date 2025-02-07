using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CasaCatolicaPuzzle;

public class ComedorPuzzle : Nivel3Puzzle
{
    [SerializeField] Asustable[] _npcs;
    [SerializeField] TocaDiscos _tocaDiscos;

    public void StartPuzzle()
    {
        _tocaDiscos.OnDiscPlay += CompletePuzzle;
    }

    void CompletePuzzle()
    {
        //activar terror bar
        //eso
        Debug.Log("<color=yellow>Puzzle Completado</color>");

        _tocaDiscos.OnDiscPlay -= CompletePuzzle;
        foreach (var npc in _npcs)
        {
            npc.GetScared(1f,-1);
        }

        GameManager.Instance.ActivateTerrorBar();

    }



}
