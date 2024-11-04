using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TornadoItems : MonoBehaviour
{
    [SerializeField] GameObject tornadoPrefab;
    [SerializeField] Transform camCenter;
    [SerializeField] float cooldown;
    [SerializeField] LayerMask trap;
    float waitCD;

    Ray ray;
    RaycastHit hit;

    //lanzarlo
    //overlapsesphere para obtener objetos cercanos
    // instanciar una "base" donde sea el centro
    // hacer que los items vayan hacia el tornado
    // que roten a su alrededor durante x segundos
    // que se lancen en direcciones random

    bool _added = false;

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
        else if (SelectorUI.habAct != 1 && _added)
        {
            SelectorUI.habilitiesManager -= Tornado;
            _added = false;
        }

        //Debug.DrawRay(camCenter.position, camCenter.forward * 5, Color.green);
        //if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 5, trap))
        //{
        //    print(hit.collider.gameObject.name);

        //}
    }

    public void Tornado(Transform myPos)
    {

        ////ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 5, trap))
        //{
        //    //Instantiate(tornadoPrefab, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 1, hit.collider.transform.position.z), Quaternion.identity);
        //    //Instantiate(tornadoPrefab, new Vector3(myPos.position.x, myPos.position.y + 1, myPos.position.z), Quaternion.identity);

        //    waitCD = 0;
        //}

        Instantiate(tornadoPrefab, new Vector3(myPos.position.x, myPos.position.y + 1, myPos.position.z), Quaternion.identity);

    }

}
