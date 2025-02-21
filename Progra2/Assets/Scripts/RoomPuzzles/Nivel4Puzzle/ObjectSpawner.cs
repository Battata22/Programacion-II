using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] Vector3 _spawnPos;
    [SerializeField] GameObject[] _objectToSpawn;

    public event DelegateType.VoidDelegate OnObjectSpawn = delegate { };
    
    void SpawnObject(int index = 0)
    {
        if(index <= -1)
        {
            StartCoroutine(SpawnAll());
            return;
        }

        var newObj = Instantiate(_objectToSpawn[index], _spawnPos, Quaternion.identity);

        OnObjectSpawn();
    }

    IEnumerator SpawnAll()
    {
        foreach (var obj in _objectToSpawn)
        {
            Instantiate(obj, _spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Interact()
    {

        SpawnObject();
    }
}
