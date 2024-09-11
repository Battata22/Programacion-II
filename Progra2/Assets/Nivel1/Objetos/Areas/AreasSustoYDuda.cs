using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasSustoYDuda : MonoBehaviour
{
    float wait;
    Vector3 muymuylejano = new Vector3(1000f, 1000f, 1000f);
    protected bool dudando = false, asustado = false;

    void Update()
    {
        wait += Time.deltaTime;

        if (wait >= 0.5f)
        {
            transform.position = muymuylejano;
        }
    }
        
    public void chito()
    {
        asustado = true;
    }
}
