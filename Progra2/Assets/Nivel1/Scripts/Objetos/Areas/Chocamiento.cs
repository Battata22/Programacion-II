using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocamiento : MonoBehaviour
{
    [SerializeField] GameObject areas, areaSonoro;

    public void Choco(Vector3 pos)
    {
        Instantiate(areas, pos, Quaternion.identity);
    }

    public void ChocoSonoro(Vector3 pos)
    {
        Instantiate(areaSonoro, pos, Quaternion.identity);
    }

}
