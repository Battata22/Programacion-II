using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public void Initialize()
    {
        //pedir a manager lista de npc para girar a verlos o algo
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Chocaste con {other.name}");
        if (other.gameObject.GetComponent<NPC>())
        {
            //other.gameObject.GetComponent<NPC>().React();
            Debug.Log($"Asustaste a {other.name}");
        }
    }
}
