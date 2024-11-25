using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersCasa2 : MonoBehaviour
{
    public bool sahumerioDentro = false;
    [SerializeField] float ogSpeed;

    void Start()
    {
        ogSpeed = GameManager.Instance.Player._speed;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sahumerioDentro && other.gameObject.GetComponent<Player>() != null)
        {

            GameManager.Instance.Player._speed = 5;
            print("enter");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (sahumerioDentro && other.gameObject.GetComponent<Player>() != null)
        {

            GameManager.Instance.Player._speed = 5;
            print("stay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (sahumerioDentro && other.gameObject.GetComponent<Player>() != null)
        {
            GameManager.Instance.Player._speed = ogSpeed;
            print("exit");
        }
    }
}
