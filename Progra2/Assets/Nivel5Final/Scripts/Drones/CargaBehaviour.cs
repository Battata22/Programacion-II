using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaBehaviour : MonoBehaviour
{
    [SerializeField] float radioExplosion, alturaDir, dmgBomba;
    [SerializeField] LayerMask maskPlayer, maskNPC;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool cochoConEspejo = false;
    [SerializeField] GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (GB_Boss.matarDrones == true)
        {
            Destroy(gameObject);
        }
    }

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
            Instantiate(explosion, transform.position, Quaternion.identity);
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
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] playerScript = Physics.OverlapSphere(transform.position, radioExplosion, maskPlayer);
        foreach (Collider collider in playerScript)
        {
            collider.gameObject.GetComponent<Player>().GetDamage();
        }
        Destroy(gameObject);
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
