using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaBehaviour : MonoBehaviour
{
    [SerializeField] float radioExplosion, alturaDir, dmgBomba;
    [SerializeField] LayerMask maskPlayer, maskNPC;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool cochoConEspejo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    //void Update()
    //{
    //    if (GameManager.Instance.GB_BossScript.fase != 2)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void FixedUpdate()
    {
        if (cochoConEspejo == true)
        {
            Regreso();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WallDefenceScript>() != null)
        {
            GameManager.Instance.GB_BossScript.GetDamage(dmgBomba);
            Destroy(gameObject);
            print("epoto");
        }

        if (other.gameObject.GetComponent<FuncionEspejo>() != null)
        {
            if(other.gameObject.GetComponent<FuncionEspejo>().agarrado == true)
            {
                print("espejito espejito");
                cochoConEspejo = true;
                other.gameObject.GetComponent<FuncionEspejo>().RomperEspejo();
            }
        }
        else if(other != null && other.gameObject.GetComponent<DroneBehaivour>() == null)
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
        foreach (Collider collider in playerScript)
        {
            collider.gameObject.GetComponent<Player>().GetDamage();
        }
        //if (playerScript[0] != null) //error aca
        //{
        //    playerScript[0].gameObject.GetComponent<Player>().GetDamage();
        //}

    }

    void Regreso()
    {
        Vector3 dir = (new Vector3(0f, alturaDir, 0f) - transform.position);
        rb.AddForce(dir * 0.3f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 5);
    }
}
