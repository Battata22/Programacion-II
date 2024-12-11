using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManagerMenu : MonoBehaviour
{
    public bool chargeDone = false;
    public int sceneElegida;
    void Start()
    {
        StartCoroutine(LoadAsyncSceneRoutine(sceneElegida));
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