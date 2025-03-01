using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazInd : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.CorazonesBehaviourScript.corazones.Add(gameObject);
        gameObject.SetActive(false);
    }

    void PickUp()
    {
        GameManager.Instance.CorazonesBehaviourScript.corazones.Remove(gameObject);
        GameManager.Instance.Player.GetHealed();
        GameManager.Instance.CorazonesBehaviourScript.Apagado();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            PickUp();
        }
    }

}
