using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GameplayModeTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] _objectToActivate;
    public delegate void PhaseChange();
    public static event PhaseChange StartGampelayPhase;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<Pickable>(out var obj) && obj._trowed)
            StartGameplay();
    }

    void StartGameplay()
    {
        foreach (var obj in _objectToActivate)
        {
            obj.SetActive(true);
        }
        StartGampelayPhase();
        gameObject.SetActive(false);
        GameManager.Instance._master2.ActivarGB();
    }
}
