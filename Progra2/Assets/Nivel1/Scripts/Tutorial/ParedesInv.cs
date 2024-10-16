using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedesInv : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Tutorial.EndWalling();
    }
}
