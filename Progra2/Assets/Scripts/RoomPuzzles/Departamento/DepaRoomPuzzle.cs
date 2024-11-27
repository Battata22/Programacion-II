using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DepaRoomPuzzle : SpecialObject
{
    [SerializeField] GameObject _pantalla;
    [SerializeField] Asustable _target;

    bool inPos = false, trapActive = false, createTrap = false;


    private void Start()
    {
        //GameManager.Instance.ActivateWinCondition += Shit;
    }

    private void Update()
    {
        var slider = GameManager.Instance._terrorBar;
        if(createTrap && slider.value >= slider.maxValue)
        {
            createTrap = false;
            CreateTrap();
        }
        if (trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (3f * 3f))
        {
            inPos = true;
        }
    }

    public void Shit()
    {
        createTrap = true;
        GameManager.Instance.ActivateWinCondition -= CreateTrap;

    }

    protected override void ObjectAbility(Transform origin)
    {
        StartCoroutine(DoSecuence());
    }

    IEnumerator DoSecuence()
    {
        _target.NpcUpdate = delegate { };

        _target.CallStopScare();

        _target.GetDoubt(_pantalla.transform.position);

        _pantalla.SetActive(true);

        yield return new WaitForSeconds(14);

        _target.CallRagdollOn();
        //_target.CallRagdollOff(1, true);

        yield return new WaitForSeconds(6);

        CallWin();
    }

    void CallWin()
    {
        GameManager.Instance.CompleteLevel();
    }

    private void OnDestroy()
    {
        GameManager.Instance.ActivateWinCondition -= CreateTrap;

    }
}
