using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterCreditos : MonoBehaviour
{
    [SerializeField] Transform creditos;
    [SerializeField] float velCreditos;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        creditos.position += (new Vector3(0f, velCreditos, 0f)) * Time.deltaTime;
    }
}
