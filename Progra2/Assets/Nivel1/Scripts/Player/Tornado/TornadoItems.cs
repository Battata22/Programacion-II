using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TornadoItems : MonoBehaviour
{
    [SerializeField] GameObject tornadoPrefab;

    //lanzarlo
    //overlapsesphere para obtener objetos cercanos
    // instanciar una "base" donde sea el centro
    // hacer que los items vayan hacia el tornado
    // que roten a su alrededor durante x segundos
    // que se lancen en direcciones random

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(tornadoPrefab, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
    }

}
