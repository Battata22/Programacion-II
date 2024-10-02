using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterHTP : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VolverMenu();
        }
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
