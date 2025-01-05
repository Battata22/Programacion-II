using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpiningWallsParentScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject[] paredesDer;
    [SerializeField] GameObject[] paredesIzq;

    void Start()
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        GirarParedes();
    }

    void GirarParedes()
    {
        foreach (var pared in paredesDer)
        {
            pared.transform.RotateAround(transform.position, transform.up, speed * Time.fixedDeltaTime);
        }

        foreach (var pared in paredesIzq)
        {
            pared.transform.RotateAround(transform.position, -transform.up, speed * Time.fixedDeltaTime);
        }
    }
}
