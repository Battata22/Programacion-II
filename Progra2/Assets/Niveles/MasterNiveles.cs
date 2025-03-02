using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterNiveles : MonoBehaviour
{
    public bool chargeDone = false, cargando = false;
    public int sceneElegida;

    void Start()
    {
        GameManager.Instance.masterNiveles = this;
    }


    void Update()
    {
        //if (cargando == false)
        //{
        //    StartCoroutine(LoadAsyncSceneRoutine(sceneElegida));
        //    cargando=true;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void ChargeDone() => chargeDone = true;
    private IEnumerator LoadAsyncSceneRoutine(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (chargeDone)
                    asyncLoad.allowSceneActivation = true;
                else
                {
                    print("ya estamos cargados");
                    yield return null;
                }
            }
            yield return null;
        }
    }
}
