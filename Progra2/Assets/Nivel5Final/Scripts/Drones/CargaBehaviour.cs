using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaBehaviour : MonoBehaviour
{
    [SerializeField] float radioExplosion;
    [SerializeField] LayerMask maskPlayer;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.gameObject.GetComponent<DroneBehaivour>() == null)
        {
            //aumentar el collider o el scale para hacer dano
            //desactivar gravedad asi queda en el lugar, desactivar el colider asi no podes dispararle, hacer un overlapsesphere para detectar, hacer efecto de explocion trucho con shader
            Explosion();

            print("bum");
        }
    }

    void Explosion()
    {
        Destroy(gameObject, 3);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        Collider[] playerScript = Physics.OverlapSphere(transform.position, radioExplosion, maskPlayer);
        if (playerScript[0] != null)
        {
            playerScript[0].gameObject.GetComponent<Player>().GetDamage();
        }

    }
}
