using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArea : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.localScale += transform.localScale / transform.localScale.x * Time.deltaTime * speed;
        if (transform.localScale.x > 7  ) Destroy(gameObject);//transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);   
    }
}
