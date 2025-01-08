using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GB_Gadget : MonoBehaviour
{
    protected Ghostbuster _myOwner;

    protected bool _isBroken = false;

    public DelegateType.VoidDelegate OnBreak = delegate { };
    public DelegateType.VoidDelegate OnRepair = delegate { };

    abstract public void GetDamage(int dmgAmount = 1);
    abstract public void Break();
    abstract public void Repair();
    public virtual void Initialize(Ghostbuster newOwner) 
    { 
        _myOwner = newOwner;
    }
    public virtual void Initialize(Ghostbuster newOwner, bool doDoubt = false)
    {
        _myOwner = newOwner;
    }


}
