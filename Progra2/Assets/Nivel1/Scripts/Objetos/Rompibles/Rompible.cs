using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rompible : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] GameObject prefabSonido;

    public void Rompe()
    {
        for(int i = 0; i < prefabs.Count; i++)
        {
            Instantiate(prefabs[i], gameObject.transform.position, Quaternion.identity);
        }
        Instantiate(prefabSonido, gameObject.transform.position, Quaternion.identity);

    }

}
