using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCuartoPadres : MonoBehaviour
{
    [SerializeField] ComedorPuzzle _conchudo;
    private void OnTriggerEnter(Collider other)
    {
        _conchudo.EnterRoom();
    }
}
