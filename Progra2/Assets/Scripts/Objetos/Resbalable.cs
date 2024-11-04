using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resbalable : MonoBehaviour
{
    bool onFloor = false;

    private void OnCollisionEnter(Collision collision)
    {
        ICanSlide _target;
        if (!onFloor && collision.gameObject.tag == "Floor")
        {
            Debug.Log("<color=cyan> Toque piso </color>");
            onFloor = true;
        }
        if(onFloor && collision.gameObject.TryGetComponent<ICanSlide>(out _target))
        {
            _target.StartSlide();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (onFloor && collision.gameObject.tag == "Floor")
        {
            Debug.Log("<color=orange> Sali del piso </color>");
            onFloor = false;
        }
    }
}
