using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFight : MonoBehaviour
{
    [SerializeField] Transform _gusMesh;
    bool _fight = false;
    [SerializeField] bool fight
    {
        get { return _fight; }
        set
        {
            _fight = value;
            if (value)
            {
                StartFight();
            }
            else
            {
                StopFight();
            }
        }
    }

    DelegateType.VoidDelegate SheAttack = delegate { };

    private void Update()
    {
        SheAttack();
        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            fight = !fight;
        }
    }

    void StartFight()
    {
        SheAttack = Fight;
    }

    void Fight()
    {
        transform.RotateAround(_gusMesh.position, Vector3.up, 90 * Time.deltaTime);
    }

    void StopFight()
    {
        SheAttack = delegate { };
    }
}
