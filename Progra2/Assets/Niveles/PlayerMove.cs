using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform[] stepPoints;
    [SerializeField] float speed;
    public int posPlayer, posDir;
    bool moving = false,der = true;
    void Start()
    {
        //print(stepPoints.Length);

    }


    void Update()
    {
        if (moving == false && Input.GetKeyDown(KeyCode.F))
        {

            //if (posPlayer == 1)
            //{
            //    //GameManager.Instance.ChargeDone();
            //    GameManager.Instance.masterNiveles.ChargeDone();
            //}
            //else
            //{
            //    SceneManager.LoadScene("Nivel" + (posPlayer + 1));
            //}

            Nivel1Fue((posPlayer + 2));

            //SceneManager.LoadScene("Nivel" + (posPlayer + 1));


        }

        if (Input.GetKeyDown(KeyCode.D) && moving == false)
        {
            moving = true;
            der = true;
            //if (posPlayer < stepPoints.Length - 1)
            //{
            //    posDir += 1;
            //}

            //PARA LIMITAR HASTA EL 5
            if (posPlayer < 4)
            {
                posDir += 1;
            }

            //if (posPlayer < 1)
            //{
            //    posDir += 1;
            //}
        }

        if (Input.GetKeyDown(KeyCode.A) && moving == false)
        {
            moving = true;
            der = false;
            if (posPlayer > 0)
            {
                posDir -= 1;
            }
        }

        if (moving)
        {
            if (der)
            {
                MoveRight(posDir);
                if (transform.position == stepPoints[posDir].position)
                {
                    moving = false;
                    posPlayer = posDir;
                }
            }
            else
            {
                MoveLeft(posDir);
                if (transform.position == stepPoints[posDir].position)
                {
                    moving = false;
                    posPlayer = posDir;
                }
            }
        }
    }

    public bool chargeDone = false, cargando = false;
    public int sceneElegida;
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
                    SceneManager.LoadScene(index);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    [SerializeField] GameObject canvasOG, canvasEspera;
    [SerializeField] SpriteRenderer player;
    [SerializeField] MeshRenderer mapa;
    public void Nivel1Fue(int escena)
    {
        StartCoroutine(LoadAsyncSceneRoutine(escena));

        //SceneManager.LoadScene("Nivel1");

        //Activar Cargando
        canvasOG.GetComponent<Canvas>().enabled = false;
        player.enabled = false;
        mapa.enabled = false;
        canvasEspera.SetActive(true);
    }

    private void OnDestroy()
    {
        //canvasOG.GetComponent<Canvas>().enabled = true;

        //player.enabled = true;
        //mapa.enabled = true;
        //canvasEspera.SetActive(false);
    }

    void MoveRight(int e)
    {
        transform.position = Vector3.MoveTowards(transform.position, stepPoints[e].position, speed * Time.deltaTime);
    }

    void MoveLeft(int e)
    {
        transform.position = Vector3.MoveTowards(transform.position, stepPoints[e].position, speed * Time.deltaTime);
    }
}
