using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform[] stepPoints;
    [SerializeField] float speed;
    public int posPlayer, posDir;
    bool moving = false,der = true;
    void Start()
    {
        //print(stepPoints.Length);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && moving == false)
        {
            moving = true;
            der = true;
            if (posPlayer < stepPoints.Length - 1)
            {
                posDir += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && moving == false)
        {
            moving = true;
            der = false;
            if (posPlayer > 0)
            {
                posDir -= 1;
            }
        }

        if (moving)
        {
            if (der)
            {
                MoveRight(posDir);
                if (transform.position == stepPoints[posDir].position)
                {
                    moving = false;
                    posPlayer = posDir;
                }
            }
            else
            {
                MoveLeft(posDir);
                if (transform.position == stepPoints[posDir].position)
                {
                    moving = false;
                    posPlayer = posDir;
                }
            }
        }
    }

    void MoveRight(int e)
    {
        transform.position = Vector3.MoveTowards(transform.position, stepPoints[e].position, speed * Time.deltaTime);
    }

    void MoveLeft(int e)
    {
        transform.position = Vector3.MoveTowards(transform.position, stepPoints[e].position, speed * Time.deltaTime);
    }
}
