using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViejaDurmiente : MonoBehaviour
{
    [SerializeField] GameObject gato, abuela, perro;
    [SerializeField] SpriteRenderer icono;
    public delegate void StartRun();
    public static event StartRun startRun = delegate { };

    void Start()
    {
        icono = GetComponentInChildren<SpriteRenderer>();
        icono.sprite = null;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Pickable>(out Pickable pickScript))
        {
            if (pickScript._trowed == true)
            {
                StartGame();
            } 
        }
    }

    void StartGame()
    {
        abuela.SetActive(true);
        //gato.SetActive(true);
        //perro.SetActive(true);

        //startRun();
        GameManager.Instance.GetComponent<PhaseManager>().CallGamePlayPhase();

        gameObject.SetActive(false);
        //GameManager.Instance.Master1.ActivarGB();
    }
}
