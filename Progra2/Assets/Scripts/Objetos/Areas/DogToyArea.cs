using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogToyArea : MonoBehaviour
{
    [SerializeField] Transform _myObject;

    public void Initialize(Transform newObject)
    {
        _myObject = newObject;

        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<DogScript>(out var dog))
        {
            dog.StartChaseToy(_myObject);
        }
    }
}
