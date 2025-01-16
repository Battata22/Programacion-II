using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
    [SerializeField] Transform _myParent;
    public Transform myParent {  get { return _myParent; } set { } }


    [SerializeField] bool _useParent;
    public bool useParent {  get { return _useParent; } set { } }

    private void Start()
    {
        _myParent = GetComponentInParent<Transform>();       
    }
}
