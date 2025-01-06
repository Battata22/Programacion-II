using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GB_Gadget : MonoBehaviour
{
    protected Ghostbuster _myOwner;

    protected bool _isBroken = false;

    abstract public void GetDamage(int dmgAmount = 1);
    abstract public void Break();
    abstract public void Repair();
    //abstract public void Initialize();
}
