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

    bool _added;

    private void Start()
    {
        waitCD = cooldown;
    }

    private void Update()
    {
        waitCD += Time.deltaTime;

        if (SelectorUI.habAct == 1 && !_added)
        {
            SelectorUI.habilitiesManager += Tornado;
            _added = true;
        }
        else if (_added)
        {
            SelectorUI.habilitiesManager -= Tornado;
            _added = false;
        }


    }

    public void Tornado()
    {
        if (waitCD >= cooldown)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(tornadoPrefab, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 1, hit.collider.transform.position.z), Quaternion.identity);
                waitCD = 0;
            }

        }
    }

}
