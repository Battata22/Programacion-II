using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasDuda : MonoBehaviour
{
    float wait;
    Vector3 muymuylejano = new Vector3(1000f, 1000f, 1000f);

    void Update()
    {
        wait += Time.deltaTime;

        if (wait >= 0.5f)
        {
            //transform.position = muymuylejano;
            Destroy(gameObject);
        }
    }
}
