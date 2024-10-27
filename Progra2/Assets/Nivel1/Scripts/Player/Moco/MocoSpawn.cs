using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MocoSpawn : MonoBehaviour
{
    public DelegateType.VoidDelegate myAction;
    Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject moco;
    [SerializeField] bool puesto = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (SelectorUI.habAct == 4 && !puesto)
        {
            SelectorUI.habilitiesManager += SetMoco;
            puesto = true;
        }
        else if (SelectorUI.habAct == 4 && puesto)
        {
            
        }
        else
        {
            SelectorUI.habilitiesManager -= SetMoco;
            puesto = false;
        }
    }

    public void SetMoco()
    {

        //ray = new Ray(new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z), transform.up);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var pos = hit.collider.transform;

            ray = new Ray(new Vector3(pos.position.x, pos.position.y + 2, pos.position.z), pos.transform.up);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Techo>())
                {
                    Instantiate(moco, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                }
            }

        }
    }

}
