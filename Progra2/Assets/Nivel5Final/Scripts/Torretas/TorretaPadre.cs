using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaPadre : MonoBehaviour
{
    public static bool laserOn = false;
    public static int activadas = 0;
    [SerializeField] float speedUp;

    void Start()
    {
      laserOn = false;
    }


    void Update()
    {
        if (laserOn == true && transform.position.y < 0)
        {
            transform.position += new Vector3(0f, speedUp * Time.deltaTime, 0f);
        }

        if(activadas == 3)
        {
            print("estan todas activadas");
        }
        else if (activadas == 2)
        {
            print("estan dos activadas");
        }
        else if (activadas == 1)
        {
            print("esta una activada");
        }
    }
}
