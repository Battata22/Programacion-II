using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TronadoPrefab : MonoBehaviour
{
    [SerializeField] float radio, fuerzaTorque, speed, rotSpeed, radioDist;
    [SerializeField] LayerMask maskTornado;
    [SerializeField] Collider[] colliders;

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskTornado);

        //Torque();

    }


    void FixedUpdate()
    {

            for (int i = 0; i < colliders.Length; i++)
            {
            //colliders[i].transform.LookAt(transform.position);
            //colliders[i].transform.right += new Vector3(rotSpeed * Time.fixedDeltaTime, 0f, 0f);

            if (Vector3.Distance(colliders[i].transform.position, transform.position) < radioDist)
            {
                Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
                rb.AddForce(transform.right * rotSpeed);


                //colliders[i].transform.position += (transform.position - colliders[i].transform.position) * Time.fixedDeltaTime * speed;


                //if (colliders[i].transform.position.y > transform.position.y + 0.5f || colliders[i].transform.position.y < transform.position.y - 0.5f)
                //{
                //    colliders[i].transform.position += new Vector3(0f, (transform.position.y - colliders[i].transform.position.y), 0f) * Time.fixedDeltaTime * speed;
                //}


            }

            }
    }

    public void Torque()
    {

        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Rigidbody>() != null && collider.GetComponent<Pickable>() != null)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();  
                rb.AddForce(transform.up * fuerzaTorque);
                rb.AddTorque(transform.right * fuerzaTorque);
            }
            
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null && other.GetComponent<Pickable>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.useGravity = false;

            //other.transform.LookAt(transform.position);
            //other.transform.RotateAround(Vector3.zero, transform.up, 10 * rotSpeed * Time.fixedDeltaTime);
            other.transform.position += (transform.position - other.transform.position) * Time.fixedDeltaTime * speed;
            //other.isTrigger = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Pickable>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.useGravity = true;
            //other.isTrigger = false;
        }
    }

}
