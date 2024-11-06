using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocoPrefab : MonoBehaviour
{
    Collider self;
    [SerializeField] AudioClip npcMoco;
    [SerializeField] GameObject charcoPrefab;

    void Start()
    {
        self = GetComponent<Collider>();
        Destroy(self.gameObject, 4f);
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Asustable>(out Asustable script))
        {
            script.GetMoco(npcMoco);
        } 

        if (other.gameObject.GetComponent<Piso>())
        {
            Instantiate(charcoPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

}
