using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] Vector3 _spawnPos;
    [SerializeField] StinkBomb[] _objectToSpawn;
    [SerializeField] StinkBomb _objectSpawned = null;
    public StinkBomb objectSpawned { get { return _objectSpawned; } }

    public event DelegateType.VoidDelegate OnObjectSpawn = delegate { };
    
    void SpawnObject(int index = 0)
    {
        if (objectSpawned != null) return;
        if(index <= -1)
        {
            StartCoroutine(SpawnAll());
            return;
        }

        var newObj = Instantiate(_objectToSpawn[index], _spawnPos, Quaternion.identity);
        _objectSpawned = newObj;
        _objectSpawned.OnExplode += EraseLastBomb;

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

    void EraseLastBomb()
    {
        _objectSpawned.OnExplode -= EraseLastBomb;
        _objectSpawned = null;
    }

    public void Interact()
    {

        SpawnObject();
    }
}
