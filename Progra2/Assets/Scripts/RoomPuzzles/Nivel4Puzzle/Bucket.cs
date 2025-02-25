using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable , IWhaterContainer
{
    [Header("<color=blue>Bucket</color>")]
    bool _hasWather;

    public event DelegateType.VoidDelegate OnGetWather = delegate { };
    public event DelegateType.VoidDelegate OnLostWather = delegate { };


    public bool CheckWather()
    {
        return _hasWather;
    }

    public void GetWather()
    {
        Debug.Log($"<color=blue>{name} LLeno de agua</color>");
        _hasWather = true;

        OnGetWather();
    }

    public void LostWather()
    {
        Debug.Log($"<color=red>{name} vacio</color>");
        _hasWather = false;

        OnLostWather();
    }
}
