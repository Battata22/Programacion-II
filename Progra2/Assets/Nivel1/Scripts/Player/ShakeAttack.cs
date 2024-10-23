using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAttack : MonoBehaviour
{
    [SerializeField] Vector3 orgPos;
    [SerializeField] float shakePotencia;
    [SerializeField] Player player;
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        orgPos = transform.localPosition;
    }


    void Update()
    {
        if (player.cameraShake == true)
        {
            transform.localPosition = orgPos + Random.insideUnitSphere * shakePotencia * Time.fixedDeltaTime;
        }
        else
        {
            transform.localPosition = orgPos;   
        }
    }
}
