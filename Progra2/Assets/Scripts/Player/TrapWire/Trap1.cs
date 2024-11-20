using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{

    private void Awake()
    {
        GameManager.Instance.firstVisualLinea = gameObject;
    }
}
