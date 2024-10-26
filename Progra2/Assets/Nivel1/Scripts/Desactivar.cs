using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivar : MonoBehaviour
{
    [SerializeField] KeyCode desactivarKey;
    [SerializeField] GameObject[] desact;
    [SerializeField] bool esta = true;

    void Update()
    {
        if (Input.GetKeyDown(desactivarKey) && esta)
        {
            foreach (GameObject obj in desact)
            {
                obj.SetActive(false);
                esta = false;
            }

            GameManager.Instance.Tutorial.EndPickUp();
            GameManager.Instance.Tutorial.EndDrop();
            GameManager.Instance.Tutorial.EndThrow();
            GameManager.Instance.Tutorial.EndInteract();
            GameManager.Instance.Tutorial.EndWalling();

        }
        else if (Input.GetKeyDown(desactivarKey) && !esta)
        {
            foreach (GameObject obj in desact)
            {
                obj.SetActive(true);
                esta = true;
            }
        }
    }
}
