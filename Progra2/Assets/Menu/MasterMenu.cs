using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.K))
        //{
        //    SceneManager.LoadScene("Nivel1", LoadSceneMode.Additive);
        //    //StartCoroutine(LoadYourAsyncScene());
        //}

    }

    //IEnumerator LoadYourAsyncScene()
    //{

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Nivel1", LoadSceneMode.);
 


    //    if (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //    else
    //    {
    //        print("cargada");
    //    }
    //}



}
