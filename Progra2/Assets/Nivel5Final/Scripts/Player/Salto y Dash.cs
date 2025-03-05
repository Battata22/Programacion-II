using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaltoyDash : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] bool midJump = false, dash = true, space = false;
    [SerializeField] float fuerzaSalto, fuerzaFaseMas1;
    [SerializeField] Image spaceBar;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuerzaSalto = 5;
        spaceBar.enabled = false;
    }


    void Update()
    {

        if (VideoIntro.terminoElVideo == true && spaceBar.enabled == false && space == false)
        {
            spaceBar.enabled = true;
            space = true;
        }

        print(GameManager.Instance.GB_BossScript.fase);

        if (GameManager.Instance.GB_BossScript.fase >= 2 && fuerzaSalto == 5)
        {
            fuerzaSalto = fuerzaFaseMas1;
        }


        if (Input.GetKeyDown(KeyCode.Space) && midJump == false)
        {
            //saltar
            Salto();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && midJump == true && dash == true)
        {
            //dash
            Dash();
            dash = false;
        }
    }

    private void FixedUpdate()
    {
        CaidaBoom();
    }
    void CaidaBoom()
    {
        if (midJump == true && rb.velocity.y <= 0)
        {
            rb.useGravity = true;
            rb.AddForce(-transform.up, ForceMode.Acceleration);
        }
        if (midJump == false)
        {
            rb.useGravity = false;
        }
    }

    void Salto()
    {
        rb.AddForce(transform.up * fuerzaSalto, ForceMode.VelocityChange);
    }

    void Dash()
    {
        audioSource.Play();
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * fuerzaSalto, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * fuerzaSalto, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * fuerzaSalto, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * fuerzaSalto, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rb.AddForce((transform.right + transform.forward).normalized * fuerzaSalto * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rb.AddForce((-transform.right + transform.forward).normalized * fuerzaSalto * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rb.AddForce((transform.right + -transform.forward).normalized * fuerzaSalto * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rb.AddForce((-transform.right + -transform.forward).normalized * fuerzaSalto * 0.1f, ForceMode.VelocityChange);
        }

        spaceBar.enabled = false;

        //rb.AddForce(transform.forward * fuerzaSalto * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if  (collision.gameObject.layer == 8)
        {
            midJump = false;
            dash = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "SueloBoss")
        {
            midJump = true;
        }
    }
}
