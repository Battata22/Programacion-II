using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Trap2 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 trap1;

    void Start()
    {
        
    }


    void Update()
    {
        var dir = trap1 - transform.position;
        ray = new Ray(dir, transform.forward);

        Debug.DrawRay(dir, transform.forward * 100000, Color.red);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Abuela")
            {
                print("abuelita vivia en peguajo");
            }

            if (hit.collider != null)
            {
                print(hit.collider.gameObject.name);
            }
        }
    }

    public void Initialize(Vector3 localTrap1)
    {
        trap1 = localTrap1;

        //if()
    }
}
