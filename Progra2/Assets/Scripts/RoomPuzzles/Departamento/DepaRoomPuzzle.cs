using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepaRoomPuzzle : SpecialObject
{
    [SerializeField] GameObject _pantalla;
    [SerializeField] Asustable _target;

    bool inPos = false, trapActive = false, createTrap = false;

    bool trampaCreada = false;

    private void Start()
    {
        //GameManager.Instance.ActivateWinCondition += Shit;
    }

    private void Update()
    {
        
        if(createTrap && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
        {
            createTrap = false;
            CreateTrap();
        }
        if (trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (3f * 3f))
        {
            inPos = true;
        }
        //if(GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
        //{
        //    CreateTrap();
        //}

        if (Input.GetKeyDown(KeyCode.M))
        {
            CallWin();
        }
    }

    public void Shit()
    {
        createTrap = true;
        GameManager.Instance.ActivateWinCondition -= CreateTrap;

    }

    public override void CreateTrap()
    {
        //if (trampaCreada) return;
        //trampaCreada = true;

        base.CreateTrap();
        //aea
        GameManager.Instance.ActivateWinCondition -= CreateTrap;

        Tutorializador();
    }

    protected override void ObjectAbility(Transform origin)
    {
        StartCoroutine(DoSecuence());
    }

    IEnumerator DoSecuence()
    {
        _target.NpcUpdate = delegate { };


        _target.CallStopScare();

        _target.GetDoubt(_pantalla.transform.position, -1);

        _target.DesactivarResets();
        _pantalla.SetActive(true);

        yield return new WaitForSeconds(14);

        _target.canRagdoll = true;
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
