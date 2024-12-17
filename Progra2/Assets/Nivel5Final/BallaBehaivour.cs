using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BallaBehaivour : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    Vector3 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }


    void FixedUpdate()
    {
        Go();
    }

    void Go()
    {
        if (dir == new Vector3(0,0,0))
        {
            dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        }
        else
        {
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision != null && collision.gameObject.GetComponent<BallaBehaivour>() == null)
        {
            print("choco con " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }

}
