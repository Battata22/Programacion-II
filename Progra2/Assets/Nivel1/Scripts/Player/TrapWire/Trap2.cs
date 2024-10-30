using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Trap2 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Transform trap1;

    [SerializeField] AudioClip clip;

    void Start()
    {
        
    }


    void Update()
    {
        var dir = trap1.position - transform.position;
        ray = new Ray(transform.position, dir);

        //Debug.DrawRay(transform.position, dir, Color.yellow);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Abuela")
            {
                print("abuelita vivia en peguajo");
                hit.transform.GetComponent<Asustable>().GetStun(clip);
            }

                //if (hit.collider != null)
                //{
                //    print(hit.collider.gameObject.name);
                //}
        }
    }

    public void Initialize(Transform localTrap1)
    {
        trap1 = localTrap1;

        //if()
    }
}
