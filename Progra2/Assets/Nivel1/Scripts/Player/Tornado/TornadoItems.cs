using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TornadoItems : MonoBehaviour
{
    [SerializeField] GameObject tornadoPrefab;
    [SerializeField] float cooldown;
    float waitCD;

    Ray ray;
    RaycastHit hit;

    //lanzarlo
    //overlapsesphere para obtener objetos cercanos
    // instanciar una "base" donde sea el centro
    // hacer que los items vayan hacia el tornado
    // que roten a su alrededor durante x segundos
    // que se lancen en direcciones random

    private void Start()
    {
        waitCD = cooldown;
    }

    private void Update()
    {
        waitCD += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && waitCD >= cooldown)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(tornadoPrefab, new Vector3(hit.point.x, transform.position.y + 1, hit.point.z), Quaternion.identity);
                waitCD = 0;
            }

        }
    }

}
