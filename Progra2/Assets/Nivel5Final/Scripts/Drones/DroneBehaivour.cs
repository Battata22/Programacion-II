using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneBehaivour : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody rb;
    [SerializeField] float cdCarga;
    [SerializeField] float speed, altura, radioDist, angulo, distanciaVentaja;
    Vector3 dir;
    [SerializeField] bool onTarget = false;
    [SerializeField] GameObject carga;
    float waitCarga;
    [SerializeField] float lifeTime, fuerzaImpulso;

    void Start()
    {
        //conseguir la info del player
        target = GameManager.Instance.Player.gameObject;
        rb = GetComponent<Rigidbody>();

        if (GB_Boss.isDroneAlive == false)
        {
            GB_Boss.isDroneAlive = true;
        }

        rb.AddForceAtPosition(transform.up * fuerzaImpulso, transform.position, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        if (onTarget == false)
        {
            dir = target.transform.position - transform.position;
        }
        else
        {
            Circular();

            waitCarga += Time.deltaTime;

            if (waitCarga >= cdCarga)
            {
                waitCarga = 0;
                SoltarCarga();
            } 
        }
        // x = 5
        // x = 5 ~~ 5 +- 0.5
        // x >= min y <= max
        if (transform.position.x >= target.transform.position.x - distanciaVentaja && transform.position.x <= target.transform.position.x + distanciaVentaja && transform.position.z >= target.transform.position.z - distanciaVentaja && transform.position.z <= target.transform.position.z + distanciaVentaja)
        {
            //print("encima");
            onTarget = true;
        }

    }

    private void FixedUpdate()
    {
        //rb.AddForce(dir.x * speed * Time.fixedDeltaTime, dir.y + altura * speed * Time.fixedDeltaTime, dir.z * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        if (onTarget == false)
        {
            GoToTarget();
        }
        else
        {
            //Circular();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallaBehaivour>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<BallaBehaivour>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BallaBehaivour>() != null)
        {
            Destroy(gameObject);
        }
    }

    void GoToTarget()
    {
        transform.position += new Vector3(dir.x, dir.y + altura, dir.z) * speed * Time.fixedDeltaTime;
    }

    void Circular()
    {

        float x = target.transform.position.x + Mathf.Cos(angulo) * radioDist;
        float y = target.transform.position.y + altura;
        float z = target.transform.position.z + Mathf.Sin(angulo) * radioDist;

        transform.position = new Vector3(x, y, z);

        angulo += speed * Time.deltaTime;
    }

    void SoltarCarga()
    {
        Instantiate(carga, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if (GB_Boss.isDroneAlive == true)
        {
            GB_Boss.isDroneAlive = false;
        }
    }
}
