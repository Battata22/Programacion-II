using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sahumerio : MonoBehaviour
{
    [SerializeField] public bool prendido = true;
    [SerializeField] bool reset = false;
    [SerializeField] int actualRoom;
    [SerializeField] float ogSpeed;


    void Start()
    {
        ogSpeed = GameManager.Instance.Player._speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (prendido && collision.transform.TryGetComponent<IFlamable>(out var flamable))
            flamable.SetOnFire();
    }

    void Update()
    {
        if (prendido && GameManager.Instance.Player.actualRoom == gameObject.GetComponent<Pickable>().actualRoom)
        {
            GameManager.Instance.Player._speed = 5;
            reset = false;
        }
        else if (prendido && GameManager.Instance.Player.actualRoom != gameObject.GetComponent<Pickable>().actualRoom && !reset)
        {
            GameManager.Instance.Player._speed = ogSpeed;
            reset = true;
        }
    }


}
