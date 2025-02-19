using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class HouseExitTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> _wantToExit = new();
    [SerializeField] Ghostbuster[] _ghostBusters;
    [SerializeField] int _exitsToGB;
    [SerializeField] int _exitsToWin;

    int internalCount;
    
    bool _gbActive = false;
    bool _barActive = false;

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
        if(internalCount >= _exitsToGB)
        {
            ActivateGB();
        }

        if(internalCount >= _exitsToWin) 
        {
            ActivateBar();
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

    void ActivateBar()
    {
        Debug.Log("<color=blue> ACTIVATE HDP </color>");
        if(_barActive) return;
        _barActive = true;
        Debug.Log("<color=blue> me active UwO </color>");

        GameManager.Instance.ActivateTerrorBar();

    }

    public void AddToList(GameObject newObj)
    {
        _wantToExit.Add(newObj);
    }
}
