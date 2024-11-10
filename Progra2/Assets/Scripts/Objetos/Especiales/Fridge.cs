using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fridge : MonoBehaviour
{
    [SerializeField] ObjectTrap _trapPrefab;
    [SerializeField] float _trapCD, _frostDist, _frostRad;
    [SerializeField] LayerMask _detectableLayers;
    public DelegateType.VoidDelegateTrans currentAbility;

    private void Awake()
    {
        CreateTrap();
    }

    public void CreateTrap()
    {
        //currentTraps++;
        currentAbility = FrostNPC;
        //crear Trampa
        var newTrap = Instantiate(_trapPrefab, transform.position + transform.forward, Quaternion.identity);
        //newTrap.OnTrapActive += TrapActivada;
        newTrap.transform.forward = transform.forward;
        newTrap.Initialize(currentAbility, _trapCD);
        //Iniciar trampa
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

    void Test(Transform tran)
    {
        Debug.Log("<color=#34d5eb> Get Frosty GIL </color>");
    }

    void FrostNPC(Transform origin)
    {
        Debug.Log("<color=#34d5eb> Get Frosty GIL </color>");
        //RaycastHit hit;
        //if(Physics.SphereCast(origin.position + new Vector3(0,1,0), _frostRad, origin.forward, out hit, _frostDist, _detectableLayers))
        //{
        //    Debug.Log($"Objeto a congelar {hit.transform.name}");
        //    if(hit.transform.TryGetComponent<ICanSlide>(out ICanSlide target))
        //    {
        //        Debug.Log("Congelado");
        //        target.StartSlide(12f);
        //    }
        //}

        Collider[] coliders = Physics.OverlapSphere(origin.position, _frostRad, _detectableLayers);
        Debug.Log(coliders.Length);
        foreach(Collider colider in coliders)
        {
            Debug.Log($"Objeto a congelar {colider.transform.name}");
            if (colider.transform.TryGetComponent<ICanSlide>(out ICanSlide target))
            {
                Debug.Log("Congelado");
                target.StartSlide(transform.forward ,12f);
            }
        }
        
    }
}
