using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruzBehaviour : MonoBehaviour
{
    [SerializeField] float pulsoCooldown, radioDetect, force;
    [SerializeField] LayerMask player;
    float wait;

    void Start()
    {
        
    }


    void Update()
    {
        wait += Time.deltaTime;
        if (wait >= pulsoCooldown)
        {
            Pushing();
            wait = 0;
        }
    }

    public void Pushing()
    {
        Collider[] collDetectados = Physics.OverlapSphere(transform.position, radioDetect, player);
        foreach (Collider collider in collDetectados)
        {
            collider.TryGetComponent<Rigidbody>(out Rigidbody _rb);
            var dir = collider.transform.position - transform.position;
            _rb.AddForce(new Vector3(dir.x, 0, dir.z).normalized * force * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
