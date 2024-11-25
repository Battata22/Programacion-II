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
    }
}
