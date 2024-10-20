using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    [SerializeField] PhysicMaterial phyParedes, phyPiso;
    [SerializeField] float radio, fuerzaTorque, tiempoInAir;
    [SerializeField] LayerMask maskUlti, maskNPC;
    [SerializeField] Image ultState;

    Player _player;

    float countDown, waitScared;
    bool active = false, used = false;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Start()
    {
        //falta: que dude mas tiempo, que se asuste con un leve delay
    }

    void Update()
    {
        countDown += Time.deltaTime;
        waitScared += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _player.nivel >= 3 && used == false)
        {
            Levitar();
        }

        if(countDown >= tiempoInAir && active == true)
        {
            Caida();
        }

        if (_player.nivel >= 3 && used == false) //GameManager.Instance.Player.nivel
        {
            //ultState.color = Color.green;
            ultState.gameObject.SetActive(true);
        }
        else if(used == true && ultState.gameObject.activeSelf) ultState.gameObject.SetActive(false);
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
                    asustableScript.GetScared(1f);

                }
            }

        }

        active = false;
    }

    void CambioFisicas()
    {

    }

}
