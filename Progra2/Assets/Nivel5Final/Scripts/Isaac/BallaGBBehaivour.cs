using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallaGBBehaivour : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if (GB_Boss.matarDrones == true)
        {
            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        Go();
    }

    void Go()
    {
        rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().GetDamage();
            //other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        if (other != null && other.gameObject.GetComponent<BallaBehaivour>() == null)
        {
            //print("choco con " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }

}
