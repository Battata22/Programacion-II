using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalasIsaac : MonoBehaviour
{
    [SerializeField] GameObject balas;
    [SerializeField] float cooldownDisparos, waitCD;
    void Update()
    {
        waitCD += Time.deltaTime;

        if (waitCD >= cooldownDisparos)
        {
            waitCD = 0;
            StartCoroutine(Shoot(cooldownDisparos));
        }

    }

    IEnumerator Shoot(float espera)
    {
        Instantiate(balas, transform.position, transform.rotation);
        yield return new WaitForSeconds(espera);
    }

}
