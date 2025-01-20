using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionIsaac : MonoBehaviour
{
    [SerializeField] float velRot;

    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, velRot * Time.deltaTime, 0f);
    }
}
