using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltRotation : MonoBehaviour
{
    [SerializeField] float speed;
    float rotation = 0;
    private void Update()
    {
        rotation += Time.deltaTime;
        if (rotation >= 360) rotation = 0;
        transform.rotation = Quaternion.Euler(0,0, rotation * speed);
    }
}
