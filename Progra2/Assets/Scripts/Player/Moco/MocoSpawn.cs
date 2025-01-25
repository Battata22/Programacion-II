using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MocoSpawn : MonoBehaviour
{
    public DelegateType.VoidDelegateTrans myAction;
    //Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject moco;
    [SerializeField] bool puesto = false;
    [SerializeField] Transform camCenter;
    [SerializeField] LayerMask trap;

    void Start()
    {
        
    }


    void Update()
    {
        #region Comment
        //if (SelectorUI.habAct == 4 && !puesto)
        //{
        //    SelectorUI.habilitiesManager += SetMoco;
        //    puesto = true;
        //    print("se añadio tu culo");
        //}
        //else if (SelectorUI.habAct == 4 && puesto)
        //{

        //}
        //else
        //{
        //    SelectorUI.habilitiesManager -= SetMoco;
        //    puesto = false;
        //    print("se saco tu culo");
        //} 
        #endregion

        if (SelectorUI.habAct == 4 && !puesto)
        {
            SelectorUI.habilitiesManager += SetMoco;
            puesto = true;
        }
        else if (SelectorUI.habAct != 4 && puesto)
        {
            SelectorUI.habilitiesManager -= SetMoco;
            puesto = false;
        }
    }

    public void SetMoco(Transform myPos)
    {

        #region Comment
        //ray = new Ray(new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z), transform.up);
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.SphereCast(camCenter.position, 0.15f, camCenter.forward, out hit, 5, trap))
        //{

        //    var pos = hit.collider.transform;

        //    ray = new Ray(new Vector3(myPos.position.x, myPos.position.y + 2, myPos.position.z), myPos.transform.up);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject.GetComponent<Techo>())
        //        {
        //            Instantiate(moco, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        //        }
        //    }
        //} 
        #endregion

        var ray = new Ray(new Vector3(myPos.position.x, myPos.position.y + 2, myPos.position.z), myPos.transform.up);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Techo>())
            {
                //Debug.Log($"<color=green> Techo encontrado, posicion do moco {hit.point}</color>");
                Instantiate(moco, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            }
        }

        #region Comment
        //if (Physics.Raycast(ray, out hit))
        //{
        //    var pos = hit.collider.transform;

        //    ray = new Ray(new Vector3(pos.position.x, pos.position.y + 2, pos.position.z), pos.transform.up);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject.GetComponent<Techo>())
        //        {
        //            Instantiate(moco, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        //        }
        //    }

        //} 
        #endregion
    }

}
