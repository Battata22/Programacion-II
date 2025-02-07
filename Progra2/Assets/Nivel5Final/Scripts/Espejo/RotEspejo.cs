using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotEspejo : MonoBehaviour
{
    [SerializeField] float velRot, alturaFlot, velFlotar;
    Vector3 ogPos;

    private void Start()
    {
        ogPos = transform.position;
    }

    void Update()
    {
        Rotar();
        Flotar();
    }

    void Rotar()
    {
        transform.eulerAngles += new Vector3(0f, velRot, 0f);
    }

    [SerializeField] bool subiendo = true;
    void Flotar()
    {
        //if (transform.position.y >= ogPos.y + alturaFlot)
        //{
        //    transform.position -= new Vector3(0f, velFlotar, 0f);
        //}
        //else if (transform.position.y <= ogPos.y - alturaFlot)
        //{
        //    transform.position += new Vector3(0f, velFlotar, 0f);
        //}

        //if (subiendo == true && transform.position.y <= ogPos.y + alturaFlot)
        //{
        //    transform.position += new Vector3(0f, velFlotar, 0f);
        //}
        //else if (subiendo == false && transform.position.y >= ogPos.y - alturaFlot)
        //{
        //    transform.position += new Vector3(0f, velFlotar, 0f);
        //}


        
        transform.position += transform.forward * Time.fixedDeltaTime;
    }

}
