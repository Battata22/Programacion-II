using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaver
{
    
    Transform _myOwner;

    public ItemSaver(Transform myOwner)
    {
        
        _myOwner = myOwner;
    }

    public void FakeUpdate()
    {
        if (_myOwner.position.y <= -10f)
        {
            _myOwner.position = GameManager.Instance.Player.transform.position + new Vector3(0,1,0);
            //_myOwner.position = new Vector3(0,0,0);
        }
    }
}
