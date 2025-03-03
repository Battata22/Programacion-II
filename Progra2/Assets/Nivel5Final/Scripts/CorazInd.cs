using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazInd : MonoBehaviour
{
    [SerializeField] AudioClip heal;

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

        Collider collider = gameObject.GetComponent<Collider>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        AudioSource source = gameObject.GetComponent<AudioSource>();

        collider.enabled = false;
        meshRenderer.enabled = false;
        source.clip = heal;
        source.Play();

        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            PickUp();
        }
    }

}
