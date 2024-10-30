using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Trap2 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Transform trap1;

    [SerializeField] AudioClip clip;
    [SerializeField] LineRenderer linea;
    //[SerializeField] GameObject lineaPrefab;

    private void Awake()
    {
        linea = Instantiate(linea, new Vector3(), Quaternion.identity);
    }

    void Update()
    {
        var dir = trap1.position - transform.position;
        ray = new Ray(transform.position, dir);

        Debug.DrawRay(transform.position, dir, Color.yellow);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.TryGetComponent<Asustable>(out var asus))
            {
                print("abuelita vivia en peguajo");
                asus.GetStun(clip);
                //hit.transform.GetComponent<Asustable>().GetStun(clip);
            }

            //if (hit.collider != null)
            //{
            //    print(hit.collider.gameObject.name);
            //}
        }

        linea.enabled = true;
        linea.SetPosition(0, trap1.position);
        linea.SetPosition(1, transform.position);
    }

    public void Initialize(Transform localTrap1)
    {
        trap1 = localTrap1;
        //if()
    }
}
