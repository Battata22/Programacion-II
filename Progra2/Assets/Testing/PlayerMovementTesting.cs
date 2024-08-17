using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementTesting : MonoBehaviour
{
    // <color=COLOR>TITULO</color> --> <color=red>Damage</color>   (es un ejemplo para acordarme como funciona :D)
    // [Tootltip("<color=green>True</color>: Explicacion del bool en True. \n<color=red>False</color>: Explicacion del bool en False")] --> el \n es para bajar la linea como enter
    [Header("<color=green>Movement</color>")] //[Header("")] Para indicar como titulo encima de algo
    [SerializeField] float speed, rotSpeed;
    float xAxis, zAxis;
    Vector3 dir = new();


    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");

        if (xAxis != 0 || zAxis != 0)
        {
            Movement(xAxis, zAxis);
        }
        //Podes rotar y moverte normalmente en vez de uno u otro como en clase, con la Q y E rotas
        if (Input.GetKey(KeyCode.E))
        {
            Rotation(1);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Rotation(-1);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Terminator();
        }
    }

    void Movement(float xAxis, float zAxis)
    {
        dir = (transform.right * xAxis + transform.forward * zAxis).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }

    void Rotation(int val) //val para indicar negativo o positivo para la direccion
    {
        transform.Rotate(0f, val * rotSpeed * Time.deltaTime, 0f);
    }

    void Terminator()
    {
        SceneManager.LoadScene("Testing");
    }
}
