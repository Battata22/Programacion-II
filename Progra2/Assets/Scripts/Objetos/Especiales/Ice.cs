using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public Transform target;
    DelegateType.VoidDelegate DoFollow = delegate { };
    Asustable asus;
    Ghostbuster gobo;

    public void Initialize(Transform newTarget)
    {
        target = newTarget;
        DoFollow = FollowTarget;

        if (target.TryGetComponent<Asustable>(out asus))
            asus.OnSlideStop += CallDestroy;
        if (target.TryGetComponent<Ghostbuster>(out gobo))
            gobo.OnSlideStop += CallDestroy;
    }

    void CallDestroy()
    {
        Debug.Log("He sido destruido");
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        DoFollow();
    }

    void FollowTarget()
    {
        transform.position = target.position;
    }

    private void OnDestroy()
    {
        if (asus != null)
            target.GetComponent<Asustable>().OnSlideStop -= CallDestroy;
        if (gobo != null)
            target.GetComponent<Ghostbuster>().OnSlideStop -= CallDestroy;
    }
}
