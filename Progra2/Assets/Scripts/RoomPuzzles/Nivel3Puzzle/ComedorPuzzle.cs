using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CasaCatolicaPuzzle;

public class ComedorPuzzle : Nivel3Puzzle
{
    [SerializeField] Asustable[] _npcs;
    [SerializeField] TocaDiscos _tocaDiscos;
    [SerializeField] Key _key;
    [SerializeField] Chest _chest;
    [SerializeField] Fireflies _chestFlies;
    [SerializeField] Fireflies _discoFlies;

    int _currentState = 0;

    //Fireflies
    [Header("<color=green>Fireflies</color>")]
    [SerializeField] Fireflies _fliesPrefab;
    bool _puzzleActive = false;
    bool _fliesActive = false;

    private void Update()
    {
        if (_puzzleActive && Input.GetKeyDown(KeyCode.V) && !_fliesActive)
        {
            _fliesActive = true;

            Fireflies newFlies;

            switch (_currentState)
            {
                case 1:
                    newFlies = Instantiate(_fliesPrefab, _key.transform.position, Quaternion.identity);
                    newFlies.SetFocusObj(_key.transform);
                    break;
                default:
                    newFlies = Instantiate(_fliesPrefab, _tocaDiscos.transform.position, Quaternion.identity);
                    newFlies.SetFocusObj(_tocaDiscos.transform);
                    break;


            }
            newFlies.OnPulseEnd += DeactivateFlies;
            newFlies.ActivateMovement(true);
        }
    }

    void DeactivateFlies()
    {
        _fliesActive = false;
    }

    public void StartPuzzle()
    {
        ChangeState();
        _puzzleActive = true;
        _tocaDiscos.OnDiscPlay += CompletePuzzle;
        _chest.OnChestOpen += ChangeState;
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


        _puzzleActive = false;
        GameManager.Instance.ActivateTerrorBar();
        Destroy(_discoFlies.gameObject);
    }

    void ChangeState()
    {
        _chest.OnChestOpen -= ChangeState;

        _currentState++;

        if (_currentState == 1)
        {
            _chestFlies.gameObject.SetActive(true);
        }
        else
            _chestFlies.gameObject.SetActive(false);
    }


}
