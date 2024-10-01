using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonInicio : MonoBehaviour
{
    [SerializeField] Transform adentro;

    public void Teleport(Transform player)
    {
        player.transform.position = adentro.position;
    }

}
