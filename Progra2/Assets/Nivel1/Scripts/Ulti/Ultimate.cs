using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    [SerializeField] float radio, fuerzaTorque, tiempoInAir;
    float countDown, waitScared;
    bool active = false, used = false;
    [SerializeField] LayerMask maskUlti, maskNPC;
    [SerializeField] Image ultState;

    void Start()
    {
        //falta: que dude mas tiempo, que se asuste con un leve delay
    }

    void Update()
    {
        countDown += Time.deltaTime;
        waitScared += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.Player._nivel >= 3 && used == false)
        {
            Levitar();
        }

        if(countDown >= tiempoInAir && active == true)
        {
            Caida();
        }

        if (GameManager.Instance.Player._nivel >= 3 && used == false)
        {
            ultState.color = Color.green;
            ultState.gameObject.SetActive(true);
        }
    }

    void Levitar()
    {
        active = true;

        used = true;

        countDown = 0;

        Collider[] collidersNPC = Physics.OverlapSphere(transform.position, radio, maskNPC);

        foreach (var collider in collidersNPC)
        {

            if (collider.GetComponent<Asustable>() != null)
            {
               Asustable asustableScript = collider.GetComponent<Asustable>();
                asustableScript.GetDoubt(collider.transform.position);

            }

        }


        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskUlti);

        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Rigidbody>() != null)
            {
                Pickable pickScript = collider.GetComponent<Pickable>();
                if(pickScript._pickedUp == false)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddForce(transform.up * fuerzaTorque);
                    rb.AddTorque(transform.right * fuerzaTorque);
                }

            }  
            
        }

    }

    void Caida()
    {
        ultState.color = Color.red;

        waitScared = 0;

        Collider[] collidersNPC = Physics.OverlapSphere(transform.position, radio, maskNPC);



        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskUlti);

        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Rigidbody>() != null)
            {
                Pickable pickScript = collider.GetComponent<Pickable>();
                if (pickScript._pickedUp == false)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb.AddForce(transform.up * -fuerzaTorque);
                }

            }

        }
        
        foreach (var collider in collidersNPC)
        {

            if (collider.GetComponent<Asustable>() != null)
            {
                if (collider.GetComponent<Asustable>() != null)
                {
                    Asustable asustableScript = collider.GetComponent<Asustable>();
                    asustableScript.GetScared();

                }
            }

        }

        active = false;
    }

}
