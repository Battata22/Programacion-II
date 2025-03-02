using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

public class PewPew : MonoBehaviour
{
    [SerializeField] bool semi = true;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootHere;
    [SerializeField] AudioSource audioSource;
    [SerializeField] VideoPlayer introPlayer;
    //public float damage
    //{
    //    get { return damage; }
    //}
    public float damage;


    void Update()
    {
        if (Time.timeScale != 0 && introPlayer.isPlaying == false)
        {
            if (semi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }
        }

    }

    void Shoot()
    {
        Instantiate(bullet, shootHere.transform.position, quaternion.identity);
        audioSource.Play();
    }
}
