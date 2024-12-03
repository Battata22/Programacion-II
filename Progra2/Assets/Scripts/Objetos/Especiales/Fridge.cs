using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Fridge : SpecialObject
{
    [SerializeField] Ice icePrefab;
    [SerializeField] GameObject partcleGen;
    [SerializeField] Asustable _target;

    bool inPos = false, trapActive = false;


    #region trash
    //public override void CreateTrap()
    //{
    //    //currentTraps++;
    //    currentAbility = ObjectAbility;
    //    //crear Trampa
    //    var newTrap = Instantiate(_trapPrefab, transform.position + transform.forward, Quaternion.identity);
    //    //newTrap.OnTrapActive += TrapActivada;
    //    newTrap.transform.forward = transform.forward;
    //    newTrap.Initialize(currentAbility, _trapCD);
    //    //Iniciar trampa
    //}

    //public void TrapActivada()
    //{

    //    trapSlider.value++;

    //    if (trapSlider.value >= trapSlider.maxValue)
    //    {
    //        SceneManager.LoadScene("Victoria");
    //    }
    //    counterTimer = 0;
    //} 
    #endregion

    private void Update()
    {
        if (trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (_detectRadius * 0.8) * (_detectRadius * 0.8))
        {
            inPos = true;
        }
    }

    protected override void ObjectAbility(Transform origin)
    {
        _target.GetDoubt(transform.position);

        trapActive = true;

        StartCoroutine(WaitToScare(origin));
    }

    IEnumerator WaitToScare(Transform origin)
    {
        while (inPos == false)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Frezze(origin);
    }

    void Frezze(Transform origin)
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

        Collider[] coliders = Physics.OverlapSphere(origin.position, _detectRadius, _detectableLayers);
        Debug.Log(coliders.Length);
        foreach (Collider colider in coliders)
        {
            Debug.Log($"Objeto a congelar {colider.transform.name}");
            if (colider.transform.TryGetComponent<ICanSlide>(out ICanSlide target))
            {
                Debug.Log("Congelado");
                target.StartSlide(transform.forward, 12f);

                var newIce = Instantiate(icePrefab, colider.transform.position, Quaternion.identity);
                newIce.Initialize(colider.transform);
            }
        }
        var newSnowGen = Instantiate(partcleGen, transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
        Destroy(newSnowGen, 2f);

        trapActive = false;
        Destroy(_trap);

    }
}
