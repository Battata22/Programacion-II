using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManagerNivel1 : MonoBehaviour
{
    public bool chargeDone = false;
    public int sceneElegida;
    void Start()
    {
        GameManager.Instance.loadNivel1 = this;
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
                    print("papito");
                    yield return null;
                }
            }
            yield return null;
        }
    }
}