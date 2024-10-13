using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathColission : MonoBehaviour
{
    Cat _cat;

    public delegate void DelegateVoidInt(int number);
    public event DelegateVoidInt OnCatCol;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cat>(out _cat))
        {
            if (_cat != null)
                OnCatCol(0);
        }
    }
}
