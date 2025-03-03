using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovettext : MonoBehaviour
{
    [SerializeField] bool chico = false, grande = false;
    [SerializeField] float speedRot;
    void Start()
    {
        
    }


    void Update()
    {
        if (chico == true)
        {
            transform.localEulerAngles += new Vector3(0f, 0f, speedRot) * Time.deltaTime;
        }

        if (grande == true)
        {
            transform.localEulerAngles += new Vector3(0f, 0f, -speedRot) * Time.deltaTime;
        }

        //transform.position += new Vector3(1f, 0f, 0f);
    }
}
