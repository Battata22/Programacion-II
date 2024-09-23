using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonInicio : MonoBehaviour
{
    [SerializeField] Vector3 adentro = new Vector3(17f, 2f, 5f);

    public void Teleport(Transform player)
    {
        player.transform.position = adentro;
    }

}
