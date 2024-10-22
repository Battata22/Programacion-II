using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedesInv : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.Tutorial.wallingTuto == true) 
        {
            GameManager.Instance.Tutorial.EndWalling();
        }
    }
}
