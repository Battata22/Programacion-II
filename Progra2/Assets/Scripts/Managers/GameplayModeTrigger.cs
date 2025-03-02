using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GameplayModeTrigger : MonoBehaviour
{
    [SerializeField]protected GameObject[] _objectToActivate;
    public delegate void PhaseChange();
    public static event PhaseChange StartGampelayPhase = delegate { };
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<Pickable>(out var obj) && obj._trowed)
            StartGameplay();
    }

    protected virtual void Start()
    {
        GameManager.Instance.pasoActual = 0;
    }

    protected virtual void StartGameplay()
    {
        foreach (var obj in _objectToActivate)
        {
            obj.SetActive(true);
        }

        GameManager.Instance.pasoActual = 1;
        //StartGampelayPhase();
        GameManager.Instance.GetComponent<PhaseManager>().CallGamePlayPhase();

        gameObject.SetActive(false);
        //GameManager.Instance._master2.ActivarGB();
    }
}
