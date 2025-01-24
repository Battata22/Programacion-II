using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class HouseExitTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> _wantToExit = new();
    [SerializeField] Ghostbuster[] _ghostBusters;
    [SerializeField] int _exitsCount;
    int internalCount;
    bool _gbActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckInList(other.gameObject))
        {
            DeactivateObject(other.gameObject);
            
        }
    }

    bool CheckInList(GameObject newObject)
    {
        bool coso = false;
        foreach(var inList in _wantToExit)
        {
            if(inList == newObject)
                coso = true;
        }
        return coso;
    }

    void DeactivateObject(GameObject coso)
    {
        _wantToExit.Remove(coso);
        coso.SetActive(false);

        internalCount++;
        if(internalCount >= _exitsCount)
        {
            ActivateGB();
        }
    }

    void ActivateGB()
    {
        if (_gbActive) return;
        _gbActive = true;

        foreach(var gb in _ghostBusters)
        {
            gb.gameObject.SetActive(true);
        }
    }

    public void AddToList(GameObject newObj)
    {
        _wantToExit.Add(newObj);
    }
}
