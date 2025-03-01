using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonesBehaviour : MonoBehaviour
{
    public List<GameObject> corazones = new List<GameObject>();
    public static bool firstHit = true;

    private void Awake()
    {
        GameManager.Instance.CorazonesBehaviourScript = this;
    }

    //private void Start()
    //{
    //    GameManager.Instance.CorazonesBehaviourScript = this;
    //}

    public void Apagado()
    {

        foreach (GameObject corazon in corazones)
        {
            corazon.SetActive(false);
        }
    }

    public void Prendido()
    {

        foreach (GameObject corazon in corazones)
        {
            corazon.SetActive(true);
        }
    }
}
