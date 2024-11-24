using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialObject : MonoBehaviour
{
    [SerializeField] protected ObjectTrap _trapPrefab;
    [SerializeField] protected float _trapCD, _frostDist, _detectRadius;
    [SerializeField] protected LayerMask _detectableLayers;
    public DelegateType.VoidDelegateTrans currentAbility;

    //Solo pora testear
    protected bool trapCrated = false;

    protected virtual void Awake()
    {
        //CreateTrap();
    }

    public virtual void CreateTrap()
    {
        //currentTraps++;
        if (trapCrated) return;

        currentAbility = ObjectAbility;
        //crear Trampa
        var newTrap = Instantiate(_trapPrefab, transform.position + transform.forward, Quaternion.identity);
        //newTrap.OnTrapActive += TrapActivada;
        newTrap.transform.forward = transform.forward;
        newTrap.Initialize(currentAbility, _trapCD);
        //Iniciar trampa

        trapCrated = true;
    }

    //public void TrapActivada()
    //{

    //    trapSlider.value++;

    //    if (trapSlider.value >= trapSlider.maxValue)
    //    {
    //        SceneManager.LoadScene("Victoria");
    //    }
    //    counterTimer = 0;
    //}

    protected virtual void Test(Transform tran)
    {
        Debug.Log("<color=#34d5eb> Get Frosty GIL </color>");
    }

    protected abstract void ObjectAbility(Transform origin);
}
