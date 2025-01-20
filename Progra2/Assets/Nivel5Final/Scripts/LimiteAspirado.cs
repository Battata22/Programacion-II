using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteAspirado : MonoBehaviour
{
    [SerializeField] float danoCD, wait;

    private void Start()
    {
        wait = danoCD;
    }

    private void Update()
    {
        wait += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (wait >= danoCD)
            {
                //apagar aspirado y hacer daño
                collision.gameObject.GetComponent<Player>().GetDamage();
                wait = 0;
            }

        }
    }

}
