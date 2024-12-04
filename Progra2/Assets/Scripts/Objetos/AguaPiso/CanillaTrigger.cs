using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanillaTrigger : MonoBehaviour
{
    [SerializeField] Asustable _granny;
    float randomWait;
    bool canActivateCanilla = false;

    LavamanoTrap _daddy;
    public event DelegateType.VoidDelegate OnCanillaBreak = delegate { };

    private void Start()
    {
        _daddy = GetComponentInParent<LavamanoTrap>();
    }

    public IEnumerator ActivarCanillaEvent()
    {
        Debug.Log("Canilla Loop");

        randomWait = Random.Range(_granny.canillaDuration * 2f, _granny.canillaDuration * 3f);

        yield return new WaitForSeconds(randomWait);

        if (canActivateCanilla)
        {
            _granny.CallCanilla(this);
            StartCoroutine(ActivarCanillaEvent());
        }
    }

    public void CallShit()
    {
        StartCoroutine(ActivarCanillaEvent());
    }

    public void EndCallEvent()
    {
        canActivateCanilla = false;
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
