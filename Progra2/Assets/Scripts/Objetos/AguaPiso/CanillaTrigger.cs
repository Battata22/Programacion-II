using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanillaTrigger : MonoBehaviour
{
    LavamanoTrap _daddy;
    public event DelegateType.VoidDelegate OnCanillaBreak = delegate { };

    private void Start()
    {
        _daddy = GetComponentInParent<LavamanoTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Pickable>(out var coso) && coso.weight > Obj_Interactuable.Weight.low && coso._trowed)
        {
            _daddy.CallAbility();
            OnCanillaBreak();
        }
    }
}
