using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Chocamiento : MonoBehaviour
{
    [SerializeField] GameObject areas;

    public void Choco(Vector3 pos)
    {
        Instantiate(areas, pos, Quaternion.identity);
    }

}
